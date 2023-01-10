// 1. Setup Project
// .NET Runtime must be 6.0+
// Make sure latest Xecta.Data.Api.Client (v2.X) is installed using NuGet
using Xecta.Data.Api.Client; // <-- Reference to Xecta api Namespace
using Xecta.Data.OpenApi.Client.Model; // <-- Reference to Xecta OpenApi DTOs
using System.Text.Json;
using Xecta.Data.Api.Client.Utils;

// Read the API client ID, Client Secret and mTls key locations from a local config file
var configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\config\config.json";
var configJson = File.ReadAllText(configFilePath);
Config? config = JsonSerializer.Deserialize<Config>(configJson);

// 2. Connect to API 
XectaApiClient? client; 
try
{
    // Setup the api, auth urls, and the mTLS certificate
    XectaApi xectaApi = new XectaApi(
        config.ApiUrl,
        config.AuthUrl,
        config.TlsPem,
        config.TlsKey);

    // Start api client session by authenticating with your client ID and Secret
    client = xectaApi.Authenticate(
        config.ApiClientId,
        config.ApiClientSecret);

    Console.WriteLine("Connection Established");
}
catch (Exception ex)
{
    // Most likely  mTLS certificate missing error 
    // or a client/secret authentication error
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 3. 3.1 Add a new well to the well header table 
// Create and UPSERT 2 well headers
var wells = new List<WellInput>
{
    new WellInput(
        sourceId: "DB-ID-FOR-WELL-001", // <-- Id of well in source database
        uwi: "00123456789120", // <-- Unique Well Identifier. In USA this is the API number. Rest of World this is company unique well id
        name: "Hello World Well 1", // <-- Name of the well
        lat: 0, // <-- Coordinates of well
        lon: 0, // <-- Coordinates of well
        group1: "Bakken Operations", // <-- Area
        group2: "Route A", // <-- Sub Area or Route
        group3: "Example Well Pad",  // <-- Platform, Cluster or Pad
        fluid: WellInput.FluidEnum.OIL, // <-- Producing fluid type (Oil, Gas Water)
        type: WellInput.TypeEnum.PRODUCER, // <-- Producing or Injecting
        liftType: WellInput.LiftTypeEnum.NATURALFLOW // <-- Lift Type
    ),

    new WellInput(
        sourceId: "DB-ID-FOR-WELL-002",
        uwi: "00123456789121",
        name: "Hello World Well 2",
        lat: 0,
        lon: 0,
        group1: "Bakken Operations",
        group2: "Route A",
        group3: "Example Well Pad",
        fluid: WellInput.FluidEnum.OIL,
        type: WellInput.TypeEnum.PRODUCER,
        liftType: WellInput.LiftTypeEnum.NATURALFLOW
    )
};

// UPSERT wells
var upsertedWells =
    await client.WellHeaderApi().UpsertWellsAsync(wells);

Console.WriteLine("Added: " + wells.Count + " wells");
foreach (var well in upsertedWells)
    Console.WriteLine(well.SourceId + " | " + well.Name);



// 3. 3.2 Get all wells from well header table
try
{
    // GET a list of well headers 
    var wellList =
        await client.WellHeaderApi().GetWellsAsync();

    Console.WriteLine("Found: " + wellList.Count + " wells");
    foreach (var wellHeader in wellList)
        Console.WriteLine(wellHeader.Name);
}
catch (Exception ex)
{
    // Most likely an authentication error
    Console.WriteLine(ex.ToString());
    Environment.Exit(1);
}

// 3. 3.3 Delete a well from the well header table
try
{
    // DELETE well header
    const string wellSourceSystemId = "DB-ID-WELL-002";
    await client.WellHeaderApi().DeleteWellBySourceAsync(wellSourceSystemId);
    Console.WriteLine("Deleted well: " + wellSourceSystemId);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    Environment.Exit(1);
}


// 4. 4.1 - Create a wellbore.
try
{
    var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    var defaultBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Main Bore ID/Primary Key from your source database
    var sideTrackBoreId = "DB-ID-WELL-001-SideTrackA"; //<-- Secondary bore ID/Primary key 

    // Create 2 well bores for this well
    var wellBores = new List<WellboreInput>();
    wellBores.Add(new WellboreInput(
            sourceWellId: sourceWellId, // <--Required. well source system ID
            sourceId: defaultBoreId, // <-- Required. wellbore source system ID
            junctionMd: 0, // <-- Required. The wellbore starts at surface
            name: "Main Bore" // <-- Required. (use "Main Bore" if no name is available)
        )
    );
    wellBores.Add(new WellboreInput(
            sourceWellId: sourceWellId, // <--Required. well source system ID
            sourceId: sideTrackBoreId, // <-- Required. wellbore source system ID
            junctionMd: 2000, // <-- Side Track bore attaches to main bore at 2000ft
            name: "SideTrack A" // <-- Required. Must specify a name for the side track
        )
    );

    // UPSERT well bore(s)
    await client.WellboreApi().UpsertWellboresAsync(wellBores);
    Console.WriteLine("Created " + wellBores.Count + " well bores for well " + sourceWellId);
}
catch (Exception ex)
{
    // Most likely the Well Source system Id did not exist in well header table
    Console.WriteLine(ex.ToString());
    Environment.Exit(1);
}


// 4. 4.2 - Get wellbore(s)
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0";
    var boresForWell = await client.WellboreApi().GetWellborBySourceAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Found " + boresForWell.Name + " wellbore for well " + sourceWellId);
}
catch (Exception ex)
{
    // Most likely the Well Source system Id did not exist in well header table
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 4. 4.3 - Delete a wellbore
try
{
    // DELETE wellbore
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-SideTrackA"; //<-- Required. SourceId of Bore being deleted
    await client.WellboreApi().DeleteWellboreBySourceAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Wellbore deleted: " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore SourceId did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 5. 5.1 - Create wellbore formation info 
try
{
    const string defaultWellId = "DB-ID-WELL-001";
    const string defaultBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var formationProperties = new List<FormationInput>
    {
        new FormationInput(
        sourceWellId: defaultWellId,
        sourceWellboreId: defaultBoreId,
        name: defaultBoreId,
        allocationFactor: 1,
        compressibilityRock: 0.000006,
        depth: 7200,
        fluidComingledGOR: 7200,
        fluidGravityApi: 38,
        fluidGravityGas: 0.78,
        fluidMolarFracCO2: 0.002,
        fluidMolarFracH2S: 0.01,
        fluidMolarFracN2: 0.002,
        fluidSalinityWater: 50000,
        porosity: 0.08,
        pressureFormationInitialDatum: 5200,
        primaryFluidType: FormationInput.PrimaryFluidTypeEnum.OIL,
        rsi: 800,
        saturationGasInitial: 0.1,
        saturationOilInitial: 0.4,
        saturationWaterInitial: 0.5,
        temperatureFormationDatum: 218,
        thicknessFormation: 180,
        volumeAcquiferInitial: 0
    )
    };

    // UPSERT new wellbore formation info
    await client.WellboreFormationApi().UpsertFormationsAsync(formationProperties);
    Console.WriteLine("Wellbore Formation Info Updated: " + defaultBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 5. 5.2 - Get wellbore formation info 
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required.  Bore ID/Primary Key from your source database
    var formationProperties = await client.WellboreFormationApi()
        .GetFormationsByWellboreAsync(sourceWellId, sourceBoreId);
    if (formationProperties != null)
        Console.WriteLine("Found Formation Info for : " + sourceBoreId + "->" + formationProperties.Name);
    else
        Console.WriteLine("No formation Info found for: " + sourceWellId + " " + sourceBoreId);

}
catch (Exception ex)
{
    // Most likely the wellbore Id did not exist
    Console.WriteLine(ex.ToString());
}


// 5. 5.3 - Delete wellbore formation info 
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required.  Bore ID/Primary Key from your source database
    await client.WellboreFormationApi().DeleteFormationBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Deleted Formation Info for : " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 6. 6.1 - Add a deviation survey
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var surveyPoints = new List<DeviationSurveyInput>();
    for (var i = 0; i < 50; i++)
    {
        surveyPoints.Add(new DeviationSurveyInput(
                sourceWellId: sourceWellId,
                sourceWellboreId: sourceBoreId,
                sourceId: "DB-ID-WELL-001-BORE-0-SURV-0" + i, //<-- Unique survey point/station id. Required
                azi: i,
                inc: i,
                md: i,
                tvd: i
            )
        );
    }

    // UPSERT Deviation survey
    Console.WriteLine("Upserting the deviation survey for : "
                      + sourceBoreId + ". This may take a few seconds.");
    await client.DeviationSurveyApi().UpsertDeviationSurveysAsync(surveyPoints);
    Console.WriteLine("Survey created for: " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 6. 6.2 - Get a deviation survey
try
{
    // GET Deviation survey
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var survey = await client.DeviationSurveyApi()
        .GetDeviationSurveyBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Found deviation survey for : " + sourceBoreId + ". Found " + survey.Count +
                      " survey points");
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 6. 6.3 - Delete a deviation survey
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    await client.DeviationSurveyApi()
        .DeleteDeviationSurveysBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Survey Deleted for " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    // If you delete a wellbore
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 7. 7.1 - Add casing to the wellbore
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var wellBoreCasing = new List<CasingInput>();
    for (var i = 0; i < 10; i++)
    {
        wellBoreCasing.Add(new CasingInput(
                sourceWellId: sourceWellId, //<-- Required
                sourceWellboreId: sourceBoreId, //<-- Required
                sourceId: "DB-ID-WELL-001-BORE-0-CAS-" + i, //<-- Required. Id of casing in source database. Must be unique
                topMd: i, //<-- Required
                bottomMd: i + 1, //<-- Required
                od: 5, // //<-- Required Outer diameter of the casing
                id: 3, // //<-- Required Inner diameter of the casing
                roughness: i, //<-- Required. Friction. Use 0 if not available
                runDate: DateTime.Today //Installation date
            )
        );
    }

    // UPSERT Casing
    Console.WriteLine("Upserting the wellbore casing for: "
                      + sourceBoreId + ". This may take a few seconds.");
    await client.WellboreCasingApi().UpsertCasingAsync(wellBoreCasing);
    Console.WriteLine("Casing created for: " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 7. 7.2 - Get casing for the wellbore

try
{
    // GET Casing
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var wellboreCasing = await client.WellboreCasingApi()
        .GetCasingBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Found casing for : " + sourceBoreId + ". Found " + wellboreCasing.Count + " Records");
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 7. 7.3 - delete casing for the wellbore

try
{
    // DELETE Casing
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    await client!.WellboreCasingApi()
        .DeleteCasingBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Deleted casing for : " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 8. 8.1 - Add tubing to the wellbore
try
{
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var wellboreTubing = new List<TubingInput>();
    for (var i = 0; i < 10; i++)
    {
        wellboreTubing.Add(new TubingInput(
                sourceWellId: sourceWellId, //<-- Required
                sourceWellboreId: sourceBoreId, //<-- Required
                sourceId: "DB-ID-WELL-001-BORE-0-TUB-" + i, //<-- Required. Must be unique for each object
                topMd: i, //<-- Required
                bottomMd: i + 1, //<-- Required
                od: 4, // //<-- Required. Outer diameter of the tubing
                id: 2, // //<-- Required. Inner diameter of the tubing
                roughness: i, //<-- Required. Friction. Use 0 if not available
                runDate: DateTime.Today //Installation date
            )
        );
    }

    // UPSERT Tubing
    Console.WriteLine("Upserting the wellbore tubing for: "
                      + sourceBoreId + ". This may take a few seconds.");
    await client.WellboreTubingApi().UpsertTubingAsync(wellboreTubing);
    Console.WriteLine("Tubing created for: " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 8. 8.2 - Get tubing for the wellbore
try
{
    // GET Tubing
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    var wellboreTubing = await client.WellboreTubingApi()
        .GetTubingForSourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Found tubing for : " + sourceBoreId + ". Found " + wellboreTubing.Count + " Records");
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
}

// 8. 8.3 - delete tubing for the wellbore
try
{
    // DELETE Tubing
    const string sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
    const string sourceBoreId = "DB-ID-WELL-001-BORE-0"; //<-- Required. Bore ID/Primary Key from your source database
    await client.WellboreTubingApi()
        .DeleteTubingBySourceWellboreAsync(sourceWellId, sourceBoreId);
    Console.WriteLine("Deleted tubing for : " + sourceBoreId);
}
catch (Exception ex)
{
    // Most likely the wellbore Id  did not exist
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 9. 9.1 -> Insert production record
try
{
    var dailyProductionInput = new DailyProductionInput(
        uwi: "00123456789120", //<-- Required. Must exist in well header table
        date: DateTime.Today, // <-- Required. 
        oilRate: 100, // <-- Required. 
        gasRate: 500, // <-- Required. 
        waterRate: 50, // <-- Required. 
        choke: 32, // <-- Required. (use 0 if not available)
        tubingPressure: 150, // <-- Required.
        casingPressure: 75, // <-- Required.
        downtimeHours: 12, // <-- Optional.(use 0 if not available)
        downtimeCode: "POWER-LOSS-PAD" //<-- Optional (use empty string or null if not available)
       //Other optional lift related inputs are available  e.g. EspFrequency
    );


    // UPSERT 1 Daily production record
    await client.ProductionApi().UpsertDailyAsync(new List<DailyProductionInput>() { dailyProductionInput });
    Console.WriteLine("Daily Production Record Created: " + dailyProductionInput.Uwi);
}
catch (Exception ex)
{
    // Most likely a required field is missing
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 9. 9.2 - Insert production history
try
{
    // Create 365 daily production records
    var productionHistory = new List<DailyProductionInput>();
    var random = new Random();
    const string uwi = "00123456789120"; // <-- Use Well UWI. Do not use SourceId
    for (var i = 365; i >= 1; i--)
    {
        productionHistory.Add(new DailyProductionInput(
            uwi: uwi,
            date: DateTime.Today.AddDays(-i), // <-- Required.
            oilRate: 100 + random.Next(50), // <-- Required.
            gasRate: 500 + random.Next(100), // <-- Required.
            waterRate: 50 + random.Next(10), // <-- Required.
            choke: 32 + random.Next(5), // <-- Required. Use 0 if not available
            tubingPressure: 150 + random.Next(30), // <-- Required.
            casingPressure: 75 + random.Next(10) // <-- Required
        ));
    }

    // UPSERT 365 daily production history records
    await client.ProductionApi().UpsertDailyAsync(productionHistory);
    Console.WriteLine("Production History Inserted for well 00123456789120");
}
catch (Exception ex)
{
    // Most likely a required field is missing
    //Error calling UpsertDaily: {\"status\":\"BAD_REQUEST\",\"message\":\
    //"Data integrity violation occurred,
    //check input arguments\",\"timestamp\":\"2022-10-10T20:59:33.125718\",\"errors\":
    //[\"Well with uwi 00123456789120 was not found in current schema\"]}"
    Console.WriteLine(ex.ToString());
    Environment.Exit(1);
}


// 9. 9.3 - Get Daily Production 
try
{
    // GET production history
    var uwi = "00123456789120";
    var productionHistory = await client.ProductionApi().GetDailyAsync(
        uwi: uwi, //<-- Use Well UWI.Do not use SourceId
        startDate: DateTime.MinValue, // <-- History start
        endDate: DateTime.Today, // <-- History end
        page: null,
        limit: 5000); // <-- Page Size 5000 (limits query to fist 5000 records)

    Console.WriteLine($"Found {productionHistory.Count} Daily Production Records for well: {uwi} ");
}
catch (Exception ex)
{
    // Most likely the well UWI did not exist in well header table
    Console.WriteLine(ex.ToString());
}

// 9. 9.4 - Delete production record

try
{
    //DELETE production history
    var uwi = "00123456789120";
    var numRecordsDeleted =
        await client.ProductionApi().DeleteDailyAsync(
            uwi: uwi,
            startDate: DateTime.Today.AddDays(-1), // <-- Yesterday
            endDate: DateTime.Today.AddDays(-1));

    Console.WriteLine("Production Record Deleted. Well:" + uwi
                                                         + " Deleted Record Count:" + numRecordsDeleted);
}
catch (Exception ex)
{
    // Most likely the UWI did not exist in well header table
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}


// 9. 9.6 - Delete production history
try
{
    // DELETE range of records
    var uwi = "00123456789120";
    var numRecordsDeleted =
        await client.ProductionApi().DeleteDailyAsync(uwi,
            DateTime.MinValue, // <-- Start
            DateTime.Today.AddDays(-1)); // <-- End;

    Console.WriteLine("Production Record Deleted. Well:" + uwi
                                                         + " Deleted Record Count:" + numRecordsDeleted);
}
catch (Exception ex)
{
    // Most likely the UWI did not exist in well header table
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}
