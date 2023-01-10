using Objects;
using Serilog.Core;
using SqlServerDataPush.Objects;
using Xecta.Data.Api.Client;
using Xecta.Data.OpenApi.Client.Model;

/// <summary>
/// Pushes a List of wells to the Xecta servers using the C# API client
/// Json config holds API keys and URL info
/// </summary>
public class ApiPusher
{
    /// <summary>
    /// Source Wells from DB
    /// </summary>
    List<SourceWell> Wells { get; set; } = new List<SourceWell>();
    /// <summary>
    /// DB Connection, SQL Stmts, and Column-to-object Map
    /// </summary>
    Config? Config { get; set; } = null;
    /// <summary>
    /// The system logger which generates the output logfile and on-screen console logging
    /// </summary>
    Logger Log { get; set; }

    /// <summary>
    /// Create instance of API Push 
    /// </summary>
    /// <param name="mapping">API Credentials</param>
    /// <param name="logger">System Logger</param>
    public ApiPusher(Config mapping, Logger logger, List<SourceWell> wellsToPush)
    {
        Config = mapping;
        Log = logger;
        Wells = wellsToPush;
    }

    /// <summary>
    /// Start a push
    /// </summary>
    public void PushWells()
    {
        if (Wells.Count == 0)
        {
            Log.Error("No wells to push");
            return;
        }
        if (!Config!.ApiPush)
        {
            Log.Warning("CONFIG: API Push is set to FALSE in config.json. No wells will be pushed.");
            return;
        }

        // 1) Connect to Xecta API   
        XectaApiClient? apiClient;
        try
        {
            XectaApi api = new XectaApi(
                Config.ApiUrl,
                Config.AuthUrl,
                Config.TlsPem,
                Config.TlsKey);

            apiClient = api.Authenticate
                (Config.ApiClientId,
                Config.ApiSecret);

            Log.Information($"API: Connected: " +
                $" Url: {Config.ApiUrl}" +
                $" ClientID:{Config.ApiClientId} " +
                $" Secret:{Config.ApiSecret}");
        }
        catch (Exception ex)
        {
            Log.Error("API CONNECTION ERROR: Unable to connect to API server: " +
                             Config.ApiUrl + " Auth: " + Config.AuthUrl + " " + ex.Message);
            return; // Did not connect. End run.
        }

        // Print out some well count stats from the API 
        var existingWellsInApi = new List<Well>();
        try
        {
            existingWellsInApi = apiClient.WellHeaderApi().GetWells();
        }
        catch (Exception ex)
        {
            Log.Error($"API ERROR: Fetching existing well collection -> {ex.Message}");
        }
        try
        {
            Log.Information($"API: {existingWellsInApi.Count} well(s) exist on the remote server");
            Log.Warning($"CONFIG: Delete existing wells = {Config.DeleteThenSync.ToString().ToUpper()}");
            if (Config.DeleteThenSync)
            {
                // Delete all existing wells from API 
                foreach (var existingWell in existingWellsInApi)
                {
                    Log.Information($"API: Deleting existing well: {existingWell.Name}, sourceId: {existingWell.SourceId}");
                    apiClient.WellHeaderApi().DeleteWellBySource(existingWell.SourceId);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error($"API ERROR: Deleting an existing well -> {ex.Message}");
        }



        // 2) Push each well to remote API 
        if (apiClient != null)
        {
            foreach (var well in Wells)
            {
                // 3) Push Well header
                try
                {
                    Log.Information($"API PUSH: Upserting well {well.Name}");
                    apiClient.WellHeaderApi().UpsertWells(new List<WellInput> { well });
                }
                catch (Exception ex)
                {
                    Log.Error($"API ERROR: Upserting well -> {ex.Message}");
                    Log.Error($"Skipping all other Upserts for this well");
                    continue; // <-- Skip all other upserts. If well has errors, its likely nothing else will push correctly.
                }


                foreach (var bore in well.WellBores)
                {
                    // 4) Push well bore
                    try
                    {
                        Log.Information($"API PUSH: Upserting Wellbore {well.Name} -> {bore.Name}");
                        apiClient.WellboreApi().UpsertWellbores(new List<WellboreInput> { bore });
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"API ERROR: Upserting wellbore -> {ex.Message}");
                    }
                    // 5) Push formations
                    try
                    {
                        Log.Information($"API PUSH: Upserting Wellbore Formation  for {well.Name} -> {bore.Name}");
                        apiClient.WellboreFormationApi().UpsertFormations(new List<FormationInput> { bore.Formation });
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"API ERROR: Upserting Tubings -> {ex.Message}");
                    }
                    // 6) Push deviation survey
                    try
                    {
                        Log.Information($"API: Upserting Deviation survey points for {well.Name} -> {bore.Name}");
                        apiClient.DeviationSurveyApi().UpsertDeviationSurveys(bore.DeviationsSurveys.ToList<DeviationSurveyInput>());
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"API ERROR: Upserting Deviaiton survey points -> {ex.Message}");
                    }
                    // 7) Push casings
                    try
                    {
                        Log.Information($"API PUSH: Upserting Casings for {well.Name} -> {bore.Name}");
                        apiClient.WellboreCasingApi().UpsertCasing(bore.Casings.ToList<CasingInput>());
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"API ERROR: Upserting Casings -> {ex.Message}");
                    }
                    // 8) Push tubings
                    try
                    {
                        Log.Information($"API PUSH: Upserting Tubings for {well.Name} -> {bore.Name}");
                        apiClient.WellboreTubingApi().UpsertTubing(bore.Tubings.ToList<TubingInput>());
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"API ERROR: Upserting Tubings -> {ex.Message}");
                    }
                }

                // 9) Push Production
                try
                {
                    Log.Information($"API: Upserting daily production for {well.Name}");

                    // Backfill Mode - Push ALL production history in 1000 record count batches
                    if (Config.UseBackFill)
                    {
                        Log.Information($"API: Using backfill batch mode");
                        var batches = well.GetDailyProductionBatches();
                        Log.Information($"API: Found {batches.Count} batches");
                        foreach (int key in batches.Keys)
                        {
                            Log.Information($"API PUSH: Upserting daily production batch [{key + 1} of {batches.Count}] for {well.Name}");
                            apiClient.ProductionApi().UpsertDaily(new List<DailyProductionInput>(batches[key]));
                        }
                    }
                    // Incremental mode - Push last 90 days
                    // Automatically overwrites any previous records for that day using
                    else
                    {
                        Log.Information($"API PUSH: Incremental Upsert last 90 days of daily production for {well.Name}");
                        var batch = well.GetDailyProductionLast90Days();
                        apiClient.ProductionApi().UpsertDaily(new List<DailyProductionInput>(batch));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"API ERROR: Upserting daily production records -> {ex.Message}");
                }
            }

        }
    }
}
