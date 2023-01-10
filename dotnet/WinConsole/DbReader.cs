using Objects;
using Serilog.Core;
using System.Data.SqlClient;
using System.Data;
using SqlServerDataPush.Objects;

/// <summary>
/// Reads well data from a SQL Server Database
/// Json config configures which tables/views to use from source Database
/// </summary>
public class DbReader
{
    /// <summary>
    /// DB Connection, SQL Stmts, and Column-to-object Map
    /// </summary>
    Config? DbMapping { get; set; } = null;

    /// <summary>
    /// The system logger which generates the output logfile and on-screen console logging
    /// </summary>
    Logger Log { get; set; }

    /// <summary>
    /// Create new instance of Source DB Engine
    /// </summary>
    /// <param name="config">DB connection info</param>
    /// <param name="logger">System Logger</param>
    public DbReader(Config config, Logger logger)
    {
        DbMapping = config;
        Log = logger;
    }

    /// <summary>
    /// Start process of generating the source well from DB
    /// </summary>
    public List<SourceWell> GetWells()
    {
        // 1) Connect to the SQL Server using the connection string from the Config
        var wellList = new List<SourceWell>();
        using SqlServer db = new();
        try
        {
            Log.Information("SOURCE DB: Connecting...");
            db.Connect(DbMapping!.Connection); // <-- Make connection using .NET Sql Conn String.
            Log.Information("SOURCE DB: Connected to: " + DbMapping.Connection);
        }
        catch (Exception ex)
        {
            Log.Error("SOURCE DB ERROR: " + ex);
            Thread.Sleep(2000);
            return wellList; // Did not connect to source DB. End run.
        }

        // 2)  Get list of wells from the source DB..
        var wells = new List<SourceWell>();
        try
        {
            var wellMap = DbMapping.CollectionMaps.Single(x => x.Collection == "Well");
            Log.Information($"*** Sql Stmt :{wellMap.Sql}");
            var wellHeadersDataTable = db.Query(wellMap.Sql);
            Log.Information($"Found {wellHeadersDataTable.Rows.Count} wells");
            wells = Utils.TableToObjectConverter<SourceWell>(wellHeadersDataTable, wellMap, null).ToList();
        }
        catch (Exception ex)
        {
            Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Well Header:  {ex.Message}");
            return wellList; // <-- Issue with source query or data type conversion. Cannot continue. End Run
        }


        // PER WELL
        foreach (var well in wells)
        {
            if (DbMapping.UseIdAsUwi)
            {
                well.Uwi = well.SourceId; // <-- UWI must be at least two characters
                if (well.Uwi.Length <= 1)
                    well.Uwi = "0" + well.Uwi;
                if (well.SourceId.Length <= 1)
                    well.SourceId = "0" + well.SourceId;
            }

            // WELL BORES
            #region well bore

            //6) Get Well bore(s) from Source DB
            if (DbMapping.UseWellAsWellbore) // <-- Some customers do not record per-bore data. In this case create a default wellbore
                well.WellBores.Add(SourceWellBore.CreateDefault(well));
            else
            {
                try
                {
                    var boreMap = DbMapping.CollectionMaps.Single(x => x.Collection == "Wellbore");
                    Log.Information($"*** Sql Stmt :{boreMap.Sql}");
                    var boresDataTable = db.Query(boreMap.Sql);
                    well.WellBores = Utils.TableToObjectConverter<SourceWellBore>(boresDataTable, boreMap, null).ToList();
                }
                catch (Exception ex)
                {
                    Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Well Bore(s):  {ex.Message}");
                    return wellList; // <-- End Run. No other collections will push if the wellbore info is not correctly identified. 
                }
            }
            Log.Information($"** WELLBORES : found {well.WellBores.Count} bore(s) for well {well.Name} **");


            // PER WELL BORE COLLECTIONS
            #region Well Bore Data collections
            foreach (var bore in well.WellBores)
            {
                Log.Information($"*** WELLBORE : well: {well.Name} -> bore: {bore.Name} ***");

                #region Casing (casing per well bore)

                try
                {
                    //6.5) Get Formation Properties from source DB..
                    // Some customers will assign defaults here instead of a DB query
                    // otherwise this is likely a join across multiple tables. Source data is likely to be sparse.
                    var formationMap = DbMapping.CollectionMaps.Single(x => x.Collection == "WellboreFormation");
                    var formationSql = formationMap.CreateSqlQuery(well.SourceId, bore.SourceId);
                    Log.Information($"*** Sql Stmt :{formationSql}");
                    var formationDataTable = db.Query(formationSql);
                    if (formationDataTable.Rows.Count > 0)
                    {
                        bore.Formation = Utils.TableToObjectConverter<SourceFormation>(formationDataTable, formationMap, bore).First(); // <-- assumes there is a formation for each bore.? 
                        bore.Formation.SetPrimaryFluid(DbMapping.DefaultFluid);
                        Log.Information($"*** FORMATION : well: {well.Name} -> bore: {bore.Name}. found formation");
                        Log.Information($"{bore.Formation.ToJson()}");
                    }
                    else
                    {
                        Log.Error($"SOURCE DB. No formation data for wellbore:  {well.Name} -> bore: {bore.Name}.");
                    }

                }
                catch (Exception ex)
                {
                    Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Wellbore Formation:  {ex.Message}");
                }

                try
                {
                    //7) Get Deviation Survey from source DB..
                    var surveyMap = DbMapping.CollectionMaps.Single(x => x.Collection == "WellboreDeviationSurvey");
                    var surveySql = surveyMap.CreateSqlQuery(well.SourceId, bore.SourceId);
                    Log.Information($"*** Sql Stmt :{surveySql}");
                    var surveyDataTable = db.Query(surveySql);
                    if (surveyDataTable.Rows.Count > 0)
                    {
                        bore.DeviationsSurveys = Utils.TableToObjectConverter<SourceDeviationSurvey>(surveyDataTable, surveyMap, bore).ToList();
                        Log.Information($"*** DEVIATION SURVEY : well: {well.Name} -> bore: {bore.Name}. found {bore.DeviationsSurveys.Count()} point(s) ***");
                        foreach (var survey in bore.DeviationsSurveys)
                            Log.Information($"{survey.ToJson()}");
                    }
                    else
                    {
                        Log.Error($"SOURCE DB. No deviation survey for:  {well.Name} -> bore: {bore.Name}.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Deviation Survey Points:  {ex.Message}");
                }

                //8) Get Casings from source DB..
                try
                {
                    var casingMap = DbMapping.CollectionMaps.Single(x => x.Collection == "WellboreCasing");
                    var casingSql = casingMap.CreateSqlQuery(well.SourceId, bore.SourceId);
                    Log.Information($"*** Sql Stmt : {casingSql}");
                    var casingDataTable = db.Query(casingSql);
                    if (casingDataTable.Rows.Count > 0)
                    {
                        bore.Casings = Utils.TableToObjectConverter<SourceCasing>(casingDataTable, casingMap, bore).ToList();
                        Log.Information($"*** CASINGS : well: {well.Name} -> bore: {bore.Name}. found {bore.Casings.Count()} item(s) ***");
                        foreach (var casing in bore.Casings)
                            Log.Information($"{casing.ToJson()}");
                    }
                    else
                    {
                        Log.Error($"SOURCE DB. No casings for:  {well.Name} -> bore: {bore.Name}.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Casings:  {ex.Message}");
                }

                //9) Get Tubings from source DB...
                try
                {
                    var tubingMap = DbMapping.CollectionMaps.Single(x => x.Collection == "WellboreTubing");
                    var tubingSql = tubingMap.CreateSqlQuery(well.SourceId, bore.SourceId);
                    Log.Information($"*** Sql Stmt :{tubingSql}");
                    var tubingDataTable = db.Query(tubingSql);
                    if (tubingDataTable.Rows.Count > 0)
                    {
                        bore.Tubings = Utils.TableToObjectConverter<SourceTubing>(tubingDataTable, tubingMap, bore).ToList();
                        Log.Information($"*** TUBINGS : well: {well.Name} -> bore: {bore.Name}. found {bore.Tubings.Count()} item(s) ***");
                        foreach (var tubing in bore.Tubings)
                            Log.Information($"{tubing.ToJson()}");
                    }
                    else
                    {
                        Log.Error($"SOURCE DB. No tubings for:  {well.Name} -> bore: {bore.Name}.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Tubings:  {ex.Message}");
                }

                #endregion

            }
            #endregion


            #endregion
            // 5) Get Daily Production from source DB...
            try
            {
                var prodMap = DbMapping.CollectionMaps.Single(x => x.Collection == "WellDailyProduction");
                var prodSql = prodMap.CreateSqlQuery(well.SourceId, string.Empty);
                Log.Information($"*** Sql Stmt :{prodSql}");
                var prodDataTable = db.Query(prodSql);
                if (prodDataTable.Rows.Count > 0)
                {
                    well.DailyProduction = Utils.TableToObjectConverter<SourceDailyProduction>(prodDataTable, prodMap, well.WellBores[0]).ToList();
                    Log.Information($"*** DAILY PRODUCTION : well: {well.Name}. found {well.DailyProduction.Count} records(s) ***");
                    foreach (var prod in well.DailyProduction)
                        Log.Information($"{prod.ToJson()}");
                }
                else
                {
                    Log.Error($"SOURCE DB. No daily production records for:  {well.Name}");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"SOURCE DB. SQL STMT OR DATA_TYPE ERROR @ Daily Production:  {ex.Message}");
            }

            // Add well to output list
            wellList.Add(well);

        }
        return wellList;
    }


    /// <summary>
    /// Implementation of a basic SQL a server .NET database connector
    /// </summary>
    internal sealed class SqlServer : IDisposable
    {
        private SqlConnection? _conn; 
        
        /// <summary>
        /// Connect to a SQL Server
        /// </summary>
        /// <param name="conn">.NET compatible Sql server connection string. Eg. 
        /// Data Source=localhost\\sqlexpress;Initial Catalog=ExampleDB; Persist Security Info=True;User ID=test; password=test  </param>
        /// <returns>true if connected</returns>
        public void Connect(string conn)
        {
            // Check connection status.
            if (_conn == null)
                _conn = new SqlConnection();

            if (_conn.State == ConnectionState.Open)
                _conn.Close();

            // Create a new Connection
            // (auto cleaned up using IDisposable)
            _conn.ConnectionString = conn;
            try
            {
                _conn.Open();
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to connect to the Sql server: " + ex.Message);
            }
        }

        /// <summary>
        /// Run a RAW sql query against the DB
        /// </summary>
        /// <param name="sql">Microsoft T-SQL Syntax</param>
        /// <returns>Datatable of results if SQL is correct, otherwise a SQL exception error</returns>
        public DataTable Query(string sql)
        {

            // Dispose command after use.
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = _conn;
            cmd.CommandText = sql;
            DataTable dt = new DataTable("query");
            var dr = cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }


        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// IDisposable
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
                if (_conn != null)
                {
                    if (_conn.State == System.Data.ConnectionState.Open)
                        _conn.Close();

                    _conn.Dispose();
                }
            }

            disposedValue = true;
        }


        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}