using System.Text.Json;
using Xecta.Data.Api.Client.Utils; // <-- Reference to Xecta API Utils

// Read the API client ID, Client Secret and mTls key locations from a local config file
var configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\config\config.json";
var configJson = File.ReadAllText(configFilePath);
Config? config = JsonSerializer.Deserialize<Config>(configJson);

// 1) Read Excel File and push content to API Server
try
{
    // Create a new Excel Reader
    var excelReader = new ExcelReader();

    // Read XLSX file from local disk
    var filePath = @"ExampleFile.xlsx";
    byte[] file = File.ReadAllBytes(filePath);
    MemoryStream ms = new MemoryStream(file);

    // Load and Validate file (this step takes a few minutes for large files)
    bool isValid = excelReader.ValidateInputData(ms);
    if (isValid)
    {
        // Connect to API server
        excelReader.ConnectToServer(config!.ApiUrl,
            config.AuthUrl,
            config.TlsPem,
            config.TlsKey,
            config.ApiClientId,
            config.ApiClientSecret);

        // Push the validated file content to the server 
        // (this step will take between 1-30 mins depending on number of wells)
        await excelReader.Push(new CancellationToken());

        // Print result log to screen
        foreach (var item in excelReader.TransactionalLogs)
            Console.WriteLine(item.Name + ": " + item.Message);
    }
}
catch (Exception ex)
{
    // Most likely  mTLS certificate missing error 
    // or a client/secret authentication error
    // or an XLSX file parsing error
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
    return;
}

