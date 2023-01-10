//
// .NET 6.0 Windows Console App to run/test the DB-to-API code.
//
using System.Text.Json;
using Serilog;
using SqlServerDataPush.Objects;

// Read DB connection and API config from config file. 
var configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\config\config.json";
var configJson = File.ReadAllText(configFilePath);
Config? config = JsonSerializer.Deserialize<Config>(configJson);

// Create a serilog Logger to log to a windows console window and to a log file
var log = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.Console()
.WriteTo.File(
    path: config!.LogFile, 
    rollingInterval: RollingInterval.Minute, 
    retainedFileCountLimit: 1)
.CreateLogger();

// Connect to source DB
var db = new DbReader(config, log);

// Get all wells from source DB
var wells = db.GetWells();

// Connect to Xecta API
var api = new ApiPusher(config, log, wells);

// Push Wells to Xecta API 
api.PushWells();

// Close log
Log.CloseAndFlush();

// Terminate the process and exit
Thread.Sleep(5000);
Environment.Exit(1);
