using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

const string dirLogsPath = "Logs";
const string fileLogs = "app.log";

var logsFolder = Path.Combine(Directory.GetCurrentDirectory(), dirLogsPath);
if (!Directory.Exists(logsFolder))
    Directory.CreateDirectory(logsFolder);


Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .WriteTo.File(Path.Combine(dirLogsPath, fileLogs))
            .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();