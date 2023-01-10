namespace SqlServerDataPush.Objects;

/// <summary>
/// Defines a Source DB to API database connnection and mapping configuration
/// </summary>
public class Config
{
    /// <summary>
    /// Location of log file
    /// </summary>
    public string LogFile { get; set; } = string.Empty;
    /// <summary>
    /// Push data to the API 
    /// </summary>
    public bool ApiPush { get; set; }
    /// <summary>
    /// Url of the API 
    /// </summary>
    public string ApiUrl { get; set; } = string.Empty;
    /// <summary>
    /// Url of the Auth Services
    /// </summary>
    public string AuthUrl { get; set; } = string.Empty;
    /// <summary>
    /// Api Client Id
    /// </summary>
    public string ApiClientId { get; set; } = string.Empty;
    /// <summary>
    /// Api Key
    /// </summary>
    public string ApiSecret { get; set; } = string.Empty;
    /// <summary>
    /// Location of TLS PEM File
    /// </summary>
    public string TlsPem { get; set; } = string.Empty;
    /// <summary>
    /// Location of TLS Key File
    /// </summary>
    public string TlsKey { get; set; } = string.Empty;
    /// <summary>
    /// .NET Database connection string
    /// </summary>
    public string Connection { get; set; } = string.Empty;

    /// <summary>
    /// Perform a full production history backfill (slow)
    /// </summary>
    public bool UseBackFill { get; set; } = true;
    /// <summary>
    /// Make the source DB ID the actual well ID 
    /// </summary>
    public bool UseIdAsUwi { get; set; } = true;
    /// <summary>
    /// True - if there is no concept of well bore in the source DB
    /// </summary>
    public bool UseWellAsWellbore { get; set; } = true;
    /// <summary>
    /// Delete everything and push data 
    /// </summary>
    public bool DeleteThenSync { get; set; } = false;
    /// <summary>
    /// Set the primary producing fluid for the asset
    /// </summary>
    public string DefaultFluid { get; set; } = "OIL";
    /// <summary>
    /// A list of which API collections map to which source table/view/query
    /// </summary>
    public List<CollectionMap> CollectionMaps { get; set; } = new();
}

/// <summary>
/// Contains the mapping configuration of DB Source to API collection
/// </summary>
public class CollectionMap
{
    /// <summary>
    /// API collection name (Well, WellBore, FormationProperties etc)
    /// </summary>
    public string Collection { get; set; } = string.Empty;
    /// <summary>
    /// The Source SQL stmt to get data from the source table/view
    /// </summary>
    public string Sql { get; set; } = string.Empty;

    /// <summary>
    /// Creates the parameterized SQL query from source SQL stmt
    /// </summary>
    /// <param name="wellId">source ID of well</param>
    /// <param name="boreId">source ID of well bore</param>
    /// <returns>sql stmt with well and well bore parameterization</returns>
    public string CreateSqlQuery(string wellId, string boreId)
    {
        var sql = Sql.Replace("{WellId}", wellId); // <-- dynamically replace well id
        sql = sql.Replace("{BoreId}", boreId); // <-- dynamically replace welbore id
        return sql;
    }

    /// <summary>
    /// List of source column mapping to the remote api properties 
    /// </summary>
    public List<ColumnMap> Columns { get; set; } = new();


}

/// <summary>
/// Defines a source column to remote API column mapping
/// </summary>
public class ColumnMap
{
    /// <summary>
    /// Which API property (!check that the source column and API data types match)
    /// </summary> 
    public string MapTo { get; set; } = string.Empty;
    /// <summary>
    /// Which DB source column  (!check column data type and API data type match)
    /// </summary>
    public string Column { get; set; } = string.Empty;
}


