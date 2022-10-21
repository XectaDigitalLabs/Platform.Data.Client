using Xecta.Data.Api.Client; // <-- Reference to Xecta api Namespace
using Xecta.Data.OpenApi.Client.Model; // <-- Reference to Xecta OpenApi DTOs
XectaApiClient? _client = null; // <--  Declare global variable to hold reference to api client

// Section 2. The connection and authenication process
try
{
    // Setup mTLS - Assign the PEM and KEY file
    // Note: These files should be stored and referenced from a secure location
    // Hardcoded this file reference for example purposes
    XectaApi xectaApi = new XectaApi(@"c:\Demo_Keys\xecta-data-api.pem", // <-- Pem file here
                                        @"c:\Demo_Keys\xecta-data-api.key", // <-- Key file here
                                        false); // <-- Use sandbox envrionment

    // Authenticate using client and secret credentials
    // Note: These credentials should come from a key vault or other secure location
    // Hardcoded credentials for example purposes
    _client = xectaApi.Authenticate("<id here>", // <-- Client ID here
                                    "<secret here>"); // <-- Client secret here

    Console.WriteLine("Connection Established");
}
catch (Exception ex)
{
    // Most likely  mTLS certificate missing error 
    // or a client/secret authentication error
    Console.WriteLine(ex.ToString());
}

// 3. 3.1 Add a new well to the well header table 
if (_client != null)
{
    try
    {
        // Create and UPSERT 2 well headers
        var wells = new List<WellInput>();
        wells.Add(new WellInput()
        {
            SourceId = "DB-ID-WELL-001", //<-- Required The Unique source system ID/Primary Key from your source database
            Uwi = "00123456789120", // <-- Required - The U.S. API Number or company assigned well ID
            Name = "Example Producer Well 1", //<-- Required
            Lat = 0, // <-- Location Co-ordinates Required
            Lon = 0, // <-- Location Co-ordinates Required
            Group1 = "Bakken Operations", //<-- Optional
            Group2 = "Route A", // <-- Optional
            Group3 = "Example Well Pad", //<-- Optional
            Fluid = WellInput.FluidEnum.OIL, //<-- Oil, Gas or Water well
            Type = WellInput.TypeEnum.PRODUCER, // <-- Producing or injecting
            LiftType = WellInput.LiftTypeEnum.NATURALFLOW //<-- Flowing natruallty or using Artificial Lift (Gas Lift, ESP, RLS)

        });
        wells.Add(new WellInput()
        {
            SourceId = "DB-ID-WELL-002",
            Uwi = "00123456789121",
            Name = "Example Producer Well 2",
            Lat = 0,
            Lon = 0,
            Group1 = "Bakken Operations",
            Group2 = "Route A",
            Group3 = "Example Well Pad",
            Fluid = WellInput.FluidEnum.OIL,
            Type = WellInput.TypeEnum.PRODUCER,
            LiftType = WellInput.LiftTypeEnum.NATURALFLOW
        });

        // UPSERT wells
        var upsertedWells =
            await _client.WellHeaderApi().UpsertWellsAsync(wells);

        Console.WriteLine("Added: " + wells.Count + " wells");
        foreach (var well in upsertedWells)
            Console.WriteLine(well.SourceId + " | " + well.Name);
    }
    catch (Exception ex)
    {
        // Most likely a requied well object field is NULL
        // e.g. Parameter 'uwi is a required property for WellInput and cannot be null
        Console.WriteLine(ex.Message);
    }
}

// 3. 3.2 Get all wells from well header table
if (_client != null)
{
    try
    {
        // GET a list of well headers 
        var wellList =
            await _client.WellHeaderApi().GetWellsAsync();

        Console.WriteLine("Found: " + wellList.Count + " wells");
        foreach (var wellHeader in wellList)
            Console.WriteLine(wellHeader.Uwi + " | " + wellHeader.Name);
    }
    catch (Exception ex)
    {
        // Most likely an authentication error
        Console.WriteLine(ex.Message);
    }
}

// 3. 3.3 Delete a well from the well header table
if (_client != null)
{
    try
    {
        // DELETE well header
        var wellSourceSystemId = "DB-ID-WELL-002";
        await _client.WellHeaderApi().DeleteWellAsync(wellSourceSystemId);
        Console.WriteLine("Deleted well: " + wellSourceSystemId);
    }
    catch (Exception ex)
    {
        // Most likely the well with Source System ID "DB-ID-WELL-002" did not exist!
        //Error calling DeleteWell:
        //{"status":"BAD_REQUEST","message":"Data integrity violation occurred, check input arguments",
        //"timestamp":"2022-09-10T18:54:41.165576",
        //"errors":["ID: "DB-ID-WELL-002" was not found"]}
        Console.WriteLine(ex.Message);
    }
}

// 3. 3.4 - Delete all well headers
if (_client != null)
{
    //try
    //{
    //    // Get a list of well headers 
    //    var allWells =
    //        await _client.WellHeaderApi().GetWellsAsync();

    //    // Delete each well (Cascade deletes all associated well header data)
    //    foreach (Well w in allWells)
    //    {
    //        await _client.WellHeaderApi().DeleteWellAsync(w.SourceId);
    //        Console.WriteLine("Deleted well: " + w.Uwi + " " + w.Name);
    //    }
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}

}

// 4. 4.1 - Create a wellbore.
if (_client != null)
{
    try
    {
        var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
        var defaultBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Main Bore ID/Primary Key from your source database
        var sideTrackBoreId = "DB-ID-WELL-001-SideTrackA"; //<-- Secondary bore ID/Primary key 

        // Create 2 well bores for this well
        var wellBores = new List<WellboreInput>();
        wellBores.Add(new WellboreInput()
        {
            SourceWellId = sourceWellId, // <--Required. well source system ID
            SourceId = defaultBoreId, // <-- Required. wellbore source system ID
            JunctionMd = 0, // <-- Required. The wellbore starts at surface
            Name = "Main Bore" // <-- Required. (use "Main Bore" if no name is available)
        });
        wellBores.Add(new WellboreInput()
        {
            SourceWellId = sourceWellId, // <--Required. well source system ID
            SourceId = sideTrackBoreId, // <-- Required. wellbore source system ID
            JunctionMd = 2000, // <-- Side Track bore attaches to main bore at 2000ft
            Name = "SideTrack A" // <-- Required. Must specify a name for the side track

        });

        // UPSERT well bore(s)
        await _client.WellboreApi().UpsertWellboresAsync(wellBores);
        Console.WriteLine("Created " + wellBores.Count + " well bores for well " + sourceWellId);
    }
    catch (Exception ex)
    {
        // Most likely the Well Source system Id did not exist in well header table
        Console.WriteLine(ex.Message);
    }
}

// 4. 4.2 - Get wellbore(s)
if (_client != null)
{
    try
    {
        var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
        var boresForWell = await _client.WellboreApi().GetWellboresByWellAsync(sourceWellId);
        Console.WriteLine("Found " + boresForWell.Count + " well bores for well " + sourceWellId);
    }
    catch (Exception ex)
    {
        // Most likely the Well Source system Id did not exist in well header table
        Console.WriteLine(ex.Message);
    }
}

// 4. 4.3 - Delete a wellbore
if (_client != null)
{
    try
    {
        // DELETE wellbore
        var sourceId = "DB-ID-WELL-001-SideTrackA"; //<-- Required. SourceId of Bore being deleted

        await _client.WellboreApi().DeleteWellboreAsync(sourceId);
        Console.WriteLine("Wellbore deleted: " + sourceId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore SourceId did not exist
        Console.WriteLine(ex.Message);
    }
}

// 5. 5.1 - Create wellbore formation info 
if (_client != null)
{
    try
    {
        var defaultBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var formationInput = new FormationInput();
        var formationProperties = new List<FormationInput>();
        formationProperties.Add(new FormationInput
        {
            SourceWellboreId = defaultBoreId,
            Name = defaultBoreId,
            AllocationFactor = 1,
            CompressibilityRock = 0.000006,
            Depth = 7200,
            FluidComingledGOR = 7200,
            FluidGravityApi = 38,
            FluidGravityGas = 0.78,
            FluidMolarFracCO2 = 0.002,
            FluidMolarFracH2S = 0.01,
            FluidMolarFracN2 = 0.002,
            FluidSalinityWater = 50000,
            Porosity = 0.08,
            PressureFormationInitialDatum = 5200,
            PrimaryFluidType = FormationInput.PrimaryFluidTypeEnum.OIL,
            Rsi = 800,
            SaturationGasInitial = 0.1,
            SaturationOilInitial = 0.4,
            SaturationWaterInitial = 0.5,
            TemperatureFormationDatum = 218,
            ThicknessFormation = 180,
            VolumeAcquiferInitial = 0
        });

        // UPSERT new wellbore formation info
        await _client.WellboreFormationApi().UpsertFormationsAsync(formationProperties);
        Console.WriteLine("Wellbore Formation Info Updated: " + defaultBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id did not exist
        Console.WriteLine(ex.Message);
    }
}

// 5. 5.2 - Get wellbore formation info 
if (_client != null)
{
    try
    {
        var defaultBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required.  Bore ID/Primary Key from your source database
        var formationProperties = await _client.WellboreFormationApi().GetFormationsByWellboreAsync(defaultBoreId);
        Console.WriteLine("Found Formation Info for : " + formationProperties.Name);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id did not exist
        Console.WriteLine(ex.Message);
    }
}

// 5. 5.3 - Delete wellbore formation info 
if (_client != null)
{
    try
    {
        var defaultBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Default Bore ID/Primary Key from your source database
        await _client.WellboreFormationApi().DeleteFormationAsync(defaultBoreId);
        Console.WriteLine("Deleted Formation Info for : " + defaultBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id did not exist
        Console.WriteLine(ex.Message);
    }
}

// 6. 6.1 - Add a deviation survey
if (_client != null)
{
    try
    {
        var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var surveyPoints = new List<DeviationSurveyInput>();
        for (int i = 0; i < 50; i++)
        {
            surveyPoints.Add(new DeviationSurveyInput()
            {
                SourceWellId = sourceWellId,
                SourceWellboreId = sourceBoreId,
                SourceId = "DB-ID-WELL-001-BORE-0-SURV-0" + i, //<-- Unique survey point/station id. Required
                Azi = i,
                Inc = i,
                Md = i,
                Tvd = i,
            });
        }

        // UPSERT Deviation survey
        Console.WriteLine("Upserting the deviation survey for : "
            + sourceBoreId + ". This may take a few seconds.");
        await _client.DeviationSurveyApi().UpsertSurveysAsync(surveyPoints);
        Console.WriteLine("Survey created for: " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 6. 6.2 - Get a deviation survey
if (_client != null)
{
    try
    {
        // GET Deviation survey
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var survey = await _client.DeviationSurveyApi().GetDeviationSurveysByWellboreAsync(sourceBoreId);
        Console.WriteLine("Found deviation survey for : " + sourceBoreId + ". Found " + survey.Count + " survey points");

    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 6. 6.3 - Delete a deviation survey
if (_client != null)
{
    try
    {
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";
        var sourceSurveyId = "DB-ID-WELL-001-BORE-0-SURV-0"; //<-- Required.
        await _client.DeviationSurveyApi().DeleteSurveyAsync(sourceSurveyId);
        Console.WriteLine("Survey Deleted for " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        // If you delete a wellbore
        Console.WriteLine(ex.Message);
    }
}


// 7. 7.1 - Add casing to the wellbore
if (_client != null)
{
    try
    {
        var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var wellBoreCasing = new List<CasingInput>();
        for (int i = 0; i < 10; i++)
        {
            wellBoreCasing.Add(new CasingInput()
            {
                SourceWellId = sourceWellId, //<-- Required
                SourceWellboreId = sourceBoreId, //<-- Required
                SourceId = "DB-ID-WELL-001-BORE-0-CAS-" + i, //<-- Required. Must be unique
                TopMd = i,//<-- Required
                BottomMd = i + 1,//<-- Required
                Od = 5, // //<-- RequiredOuter diameter of the casing
                Id = 3, // //<-- RequiredInner diameter of the casing
                Roughness = i, //<-- Required. Friction. Use 0 if not available
                RunDate = DateTime.Today //Installation date
            });
        }

        // UPSERT Casing
        Console.WriteLine("Upserting the wellbore casing for: "
            + sourceBoreId + ". This may take a few seconds.");
        await _client.WellboreCasingApi().UpsertCasingAsync(wellBoreCasing);
        Console.WriteLine("Casing created for: " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 7. 7.2 - Get casing for the wellbore
if (_client != null)
{
    try
    {
        // GET Casing
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var wellboreCasing = await _client.WellboreCasingApi().GetCasingForSourceWellboreAsync(sourceBoreId);
        Console.WriteLine("Found casing for : " + sourceBoreId + ". Found " + wellboreCasing.Count + " Records");
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 7. 7.3 - delete casing for the wellbore
if (_client != null)
{
    try
    {
        // DELETE Casing
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        await _client.WellboreCasingApi().DeleteCasingAsync(sourceBoreId);
        Console.WriteLine("Deleted casing for : " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 8. 8.1 - Add tubing to the wellbore
if (_client != null)
{
    try
    {
        var sourceWellId = "DB-ID-WELL-001"; //<-- Required. Well ID/Primary Key from your source database
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var wellboreTubing = new List<TubingInput>();
        for (int i = 0; i < 10; i++)
        {
            wellboreTubing.Add(new TubingInput()
            {
                SourceWellId = sourceWellId, //<-- Required
                SourceWellboreId = sourceBoreId, //<-- Required
                SourceId = "DB-ID-WELL-001-BORE-0-TUB-" + i, //<-- Required. Must be unique for each object
                TopMd = i,//<-- Required
                BottomMd = i + 1,//<-- Required
                Od = 4, // //<-- Required. Outer diameter of the tubing
                Id = 2, // //<-- Required. Inner diameter of the tubing
                Roughness = i, //<-- Required. Friction. Use 0 if not available
                RunDate = DateTime.Today //Installation date
            });
        }

        // UPSERT Tubing
        Console.WriteLine("Upserting the wellbore tubing for: "
            + sourceBoreId + ". This may take a few seconds.");
        await _client.WellboreTubingApi().UpsertTubingAsync(wellboreTubing);
        Console.WriteLine("Tubing created for: " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 8. 8.2 - Get tubing for the wellbore
if (_client != null)
{
    try
    {
        // GET Tubing
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        var wellboreTubing = await _client.WellboreTubingApi().GetTubingForSourceWellboreAsync(sourceBoreId);
        Console.WriteLine("Found tubing for : " + sourceBoreId + ". Found " + wellboreTubing.Count + " Records");
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 8. 8.3 - delete tubing for the wellbore
if (_client != null)
{
    try
    {
        // DELETE Tubing
        var sourceBoreId = "DB-ID-WELL-001-BORE-0";//<-- Required. Bore ID/Primary Key from your source database
        await _client.WellboreTubingApi().DeleteTubingAsync(sourceBoreId);
        Console.WriteLine("Deleted tubing for : " + sourceBoreId);
    }
    catch (Exception ex)
    {
        // Most likely the wellbore Id  did not exist
        Console.WriteLine(ex.Message);
    }
}

// 9. 9.1 -> Insert production record
if (_client != null)
{
    try
    {

        DailyProductionInput dailyProductionInput = new DailyProductionInput()
        {
            Uwi = "00123456789120", //<-- Required. Must exist in well header table
            Date = DateTime.Today, // <-- Required. 
            OilRate = 100, // <-- Required. 
            GasRate = 500, // <-- Required. 
            WaterRate = 50, // <-- Required. 
            Choke = 32, // <-- Required. (use 0 if not available)
            TubingPressure = 150, // <-- Required.
            CasingPressure = 75, // <-- Required.
            DowntimeHours = 12, // <-- Optional.(use 0 if not available)
            DowntimeCode = "POWER-LOSS-PAD", //<-- Optional (use empty string or null if not avaialable)
            //Other optional lift related inputs are available  e.g. EspFrequency
        };

        // UPSERT 1 Daily production record
        await _client.ProductionApi().UpsertDailyAsync(new List<DailyProductionInput>() { dailyProductionInput });
        Console.WriteLine("Daily Production Record Created: " + dailyProductionInput.Uwi);
    }
    catch (Exception ex)
    {
        // Most likely a required field is missing
        Console.WriteLine(ex.Message);
    }
}

// 9. 9.2 - Insert production history
if (_client != null)
{
    try
    {
        // Create 365 daily production records
        List<DailyProductionInput> productionHistory = new List<DailyProductionInput>();
        var random = new Random();
        var uwi = "00123456789120"; // <-- Use Well UWI. Do not use SourceId
        for (int i = 365; i >= 1; i--)
        {
            productionHistory.Add(new DailyProductionInput()
            {
                Uwi = uwi,
                Date = DateTime.Today.AddDays(-i), // <-- Required.
                OilRate = 100 + random.Next(50), // <-- Required.
                GasRate = 500 + random.Next(100), // <-- Required.
                WaterRate = 50 + random.Next(10), // <-- Required.
                Choke = 32 + random.Next(5), // <-- Required. Use 0 if not available
                TubingPressure = 150 + random.Next(30), // <-- Required.
                CasingPressure = 75 + random.Next(10), // <-- Required
            });
        }

        // UPSERT 365 daily production history records
        await _client.ProductionApi().UpsertDailyAsync(productionHistory);
        Console.WriteLine("Production History Inserted for well 00123456789120");
    }
    catch (Exception ex)
    {
        // Most likely a required field is missing
        //Error calling UpsertDaily: {\"status\":\"BAD_REQUEST\",\"message\":\
        //"Data integrity violation occurred,
        //check input arguments\",\"timestamp\":\"2022-10-10T20:59:33.125718\",\"errors\":
        //[\"Well with uwi 00123456789120 was not found in current schema\"]}"
        Console.WriteLine(ex.Message);
    }
}

// 9. 9.3 - Get Daily Production 
if (_client != null)
{
    try
    {
        // GET production history
        var uwi = "00123456789120";
        var productionHistory = await _client.ProductionApi().GetDailyAsync(
            uwi, //<-- Use Well UWI.Do not use SourceId
            DateTime.MinValue,// <-- History start
            DateTime.Today,// <-- History end
            null, 5000); // <-- Page Size 5000 (limits query to fist 5000 records)

        Console.WriteLine("Found " + productionHistory.Count() +
            " Daily Production Records for well: " + uwi);
    }
    catch (Exception ex)
    {
        // Most likely the well UWI did not exist in well header table
        Console.WriteLine(ex.Message);
    }
}

// 9. 9.4 - Delete production record
if (_client != null)
{
    try
    {
        //DELETE production history
        var uwi = "00123456789120";
        var numRecordsDeleted =
            await _client.ProductionApi().DeleteDailyAsync(uwi,
                                            DateTime.Today.AddDays(-1),  // <-- Yesterday
                                            DateTime.Today.AddDays(-1));

        Console.WriteLine("Production Record Deleted. Well:" + uwi
                        + " Deleted Record Count:" + numRecordsDeleted);
    }
    catch (Exception ex)
    {
        // Most likely the UWI did not exist in well header table
        Console.WriteLine(ex.Message);
    }
}

// 9. 9.6 - Delete production history
if (_client != null)
{
    try
    {
        // DELETE range of records
        var uwi = "00123456789120";
        var numRecordsDeleted =
            await _client.ProductionApi().DeleteDailyAsync(uwi,
                                            DateTime.MinValue,  // <-- Start
                                            DateTime.Today.AddDays(-1));// <-- End;

        Console.WriteLine("Production Record Deleted. Well:" + uwi
                        + " Deleted Record Count:" + numRecordsDeleted);
    }
    catch (Exception ex)
    {
        // Most likely the UWI did not exist in well header table
        Console.WriteLine(ex.Message);
    }
}