using Xecta.Data.Api.Client; // <-- Reference to Xecta api Namespace
using Xecta.Data.OpenApi.Client.Model; // <-- Reference to Xecta OpenApi DTOs
XectaApiClient? _client = null; // <--  Declare global variable to hold reference to api client

//Section 2. The connection and authenication process
try
{


    // Setup mTLS - Assign the PEM and KEY file
    // Note: These files should be stored and referenced from a secure location
    // Hardcoded this file reference for example purposes
    XectaApi xectaApi = new XectaApi(@"c:\Demo_Keys\xecta-data-api.pem", // <-- Pem file here
                                        @"c:\Demo_Keys\xecta-data-api.key", // <-- Key file here
                                        false); // <-- sandbox 

    // Authenticate using client and secret credentials
    // Note: These credentials should come from a key vault or other secure location
    // Hardcoded credentials for example purposes
    _client = xectaApi.Authenticate("<your client id>", // <-- Client ID here
                                    "<your client secret>"); // <-- Client secret here

    Console.WriteLine("API client is connected.");
}
catch (Exception ex) // Most likely a client/secret authentication error
{
    //System.Security.Authentication.AuthenticationException: Api and Secret key seems to be invalid
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Client is connected: " + _client?.IsConnected);

// Section 3. -> 3.1 Add a new well to the well header table 
if (_client != null)
{
    try
    {

        // Create a list of wells to be UPSERTED
        // Note: This list would likely come from a source system/database
        // This simple example creates a list of 2 wells
        List<WellInput> wells = new List<WellInput>();
        wells.Add(new WellInput()
        {
            Uwi = "00123456789120", // <-- Required
            Name = "Example Producer Well 1", //<-- Required
            Lat = 0, // <-- Required
            Lon = 0, // <-- Required
            Group1 = "Bakken Operations", //<-- Optional
            Group2 = "Route A", // <-- Optional
            Group3 = "Example Well Pad", //<-- Optional
            Fluid = WellInput.FluidEnum.OIL,
            Type = WellInput.TypeEnum.PRODUCER,
            LiftType = WellInput.LiftTypeEnum.NATURALFLOW

        });
        wells.Add(new WellInput()
        {
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

        // Send well list to the server
        var result =
            await _client.WellHeaderApi().UpsertWellsAsync(wells);

        // Print each well that was added to the console output window
        Console.WriteLine("Added: " + wells.Count + " wells");
        foreach (var well in result)
            Console.WriteLine(well.Xid + " | " + well.Name);
    }
    catch (Exception ex) // Most likely a requied well object field is NULL
    {
        // e.g. Parameter 'uwi is a required property for WellInput and cannot be null
        Console.WriteLine(ex.Message);
    }
}

// Section 3. 3.2 Get all wells from well header table
if (_client != null)
{
    try
    {
        // Get a list of well headers from server
        var allWells =
            await _client.WellHeaderApi().GetWellsAsync();

        // Print each well to console output window
        Console.WriteLine("Found: " + allWells.Count + " wells");
        foreach (var well in allWells)
            Console.WriteLine(well.Xid + " | " + well.Name);
    }
    catch (Exception ex) // Most likely an authentication error
    {
        Console.WriteLine(ex.Message);
    }
}

// Section 3.-> 3.3 Delete a well from the well header table
if (_client != null)
{
    try
    {
        // Delete a well from the well header table
        var deleteThisUwi = "00123456789120";
        var deleteRowCount =
            await _client.WellHeaderApi().DeleteWellAsync(deleteThisUwi);

        // Write result to console output window
        Console.WriteLine(deleteRowCount + " Rows deleted. Deleted well: " + deleteThisUwi);
    }
    catch (Exception ex) // Most likely the well with uwi 00123456789120 did not exist!
    {
        // e.g 
        //Error calling DeleteWell:
        //{"status":"BAD_REQUEST","message":"Data integrity violation occurred, check input arguments",
        //"timestamp":"2022-09-10T18:54:41.165576",
        //"errors":["UWI: 00123456789120 was not found"]}
        Console.WriteLine(ex.Message);
    }
}

// 3.4 - Delete all well headers!
if (_client != null)
{
    //try
    //{
    //    // Get a list of well headers from server
    //    var allWells =
    //        await _client.WellHeaderApi().GetWellsAsync();

    //    // Delete each well (Cascade deletes all associated well header data)
    //    foreach (Well w in allWells)
    //    {
    //        await _client.WellHeaderApi().DeleteWellAsync(w.Uwi);
    //        Console.WriteLine("Deleted well: " + w.Uwi + " " + w.Name);
    //    }
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}

}

// 4. 4.1 -> Insert production record
if (_client != null)
{
    try
    {
        // Create example daily production record
        // If a record already exists for this calendar day it will be overwritten using UPSERT
        DailyProductionInput dailyProductionInput = new DailyProductionInput()
        {
            Uwi = "00123456789121", //<-- Required. Must exist in well header table
            Date = DateTime.Today, // <-- Required.
            OilRate = 100, // <-- Required.
            GasRate = 500, // <-- Required.
            WaterRate = 50, // <-- Required.
            Choke = 32, // <-- Required.
            TubingPressure = 150, // <-- Required.
            CasingPressure = 75, // <-- Required
            DowntimeHours = 12,
            DowntimeCode = "POWER-LOSS-PAD",
            // Note: Other optional lift related inputs exist e.g. EspFrequency
        };

        // Add single daily production record for well : 00123456789121
        var prodRec =
            await _client.ProductionApi().UpsertDailyAsync(new List<DailyProductionInput>() { dailyProductionInput });
        Console.WriteLine("Production Record Created: " + prodRec[0].Xid + " Date:" + prodRec[0].Date.ToString());
    }
    catch (Exception ex) // Most likely a required field is missing
    {
        Console.WriteLine(ex.Message);
    }

}

// 4. 4.2 - Insert production history
if (_client != null)
{
    try
    {
        // Create 365 example daily production records
        List<DailyProductionInput> productionHistory = new List<DailyProductionInput>();
        Random r = new Random();
        for (int i = 365; i >= 1; i--)
        {
            productionHistory.Add(new DailyProductionInput()
            {
                Uwi = "00123456789121", //<-- Required. Must exist in well header table
                Date = DateTime.Today.AddDays(-i), // <-- Required.
                OilRate = 100 + r.Next(50), // <-- Required.
                GasRate = 500 + r.Next(100), // <-- Required.
                WaterRate = 50 + r.Next(10), // <-- Required.
                Choke = 32 + r.Next(5), // <-- Required.
                TubingPressure = 150 + r.Next(30), // <-- Required.
                CasingPressure = 75 + r.Next(10), // <-- Required
            });
        }

        // Add production history for well : 00123456789121
        var insertedRecs =
            await _client.ProductionApi().UpsertDailyAsync(productionHistory);

        Console.WriteLine("Production History Inserted: "
            + "Record Count: " + insertedRecs.Count());
    }
    catch (Exception ex) // Most likely a required field is missing
    {
        //Error calling UpsertDaily: {\"status\":\"BAD_REQUEST\",\"message\":\
        //"Data integrity violation occurred,
        //check input arguments\",\"timestamp\":\"2022-10-10T20:59:33.125718\",\"errors\":
        //[\"Well with uwi 0012345678912 was not found in current schema\"]}"
        Console.WriteLine(ex.Message);
    }
}


// 4. 4.4 - Get Daily Production (history)
if (_client != null)
{
    try
    {

        // Get all production history for well : 00123456789121
        var getThisUwi = "00123456789121";
        var recs =
            await _client.ProductionApi().GetDailyAsync(getThisUwi,
                                            DateTime.MinValue,  // <-- Beginning of time
                                            DateTime.Today,// <-- Today
                                             null, 5000); // <-- Page Size 5000 (limits query to fist 5000 records)

        Console.WriteLine("Found " + recs.Count() +
            " Daily Production Records for : " + getThisUwi);
    }
    catch (Exception ex) // Most likely the UWI did not exist in well header table
    {
        Console.WriteLine(ex.Message);
    }
}

// 4. 4.5 - Delete production record
if (_client != null)
{
    try
    {

        // Delete production record for well : 00123456789121
        var deletedRecCount =
            await _client.ProductionApi().DeleteDailyAsync("00123456789121",
                                            DateTime.Today.AddDays(-1),  // <-- Yesterday
                                            DateTime.Today.AddDays(-1));

        Console.WriteLine("Production Record Deleted. " +
            "Deleted Record Count: " + deletedRecCount);
    }
    catch (Exception ex) // Most likely the UWI did not exist in well header table
    {
        Console.WriteLine(ex.Message);
    }
}

// 4. 4.6 - Delete production history
if (_client != null)
{
    try
    {

        // Delete production record for well : 00123456789121
        var deletedRecCount =
            await _client.ProductionApi().DeleteDailyAsync("00123456789121",
                                            DateTime.MinValue,  // <-- Beginning of time
                                            DateTime.Today); // <-- Today

        Console.WriteLine("Production Records Deleted. " +
            "Deleted Record Count: " + deletedRecCount);
    }
    catch (Exception ex) // Most likely the UWI did not exist in well header table
    {
        Console.WriteLine(ex.Message);
    }
}
