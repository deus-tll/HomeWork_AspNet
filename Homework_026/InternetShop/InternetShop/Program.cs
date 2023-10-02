using Library.Interfaces;
using Library.Models.ContextModels;
using Library.Models.HandlerModels;
using Library.Models.InitializeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication("InternetShop_CookieAuthenticationScheme").AddCookie("InternetShop_CookieAuthenticationScheme", options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.LogoutPath = new PathString("/Account/Logout");
});

builder.Services.AddScoped<IUsersHandler, UsersHandler>();
builder.Services.AddScoped<IProductsHandler, ProductsHandler>();

const string dirLogsPath = "Logs";
const string fileLogs = "app.log";

var logsFolder = Path.Combine(Directory.GetCurrentDirectory(), dirLogsPath);
if (Directory.Exists(logsFolder))
    Directory.CreateDirectory(logsFolder);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.File(Path.Combine(dirLogsPath, fileLogs))
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var config = services.GetRequiredService<IConfiguration>();
    var logger = services.GetRequiredService<ILogger<ProductsInitializer>>();

    var usersInitializer = new UsersInitializer(userManager, roleManager, config);
    await usersInitializer.InitializeAsync();

    string url = config["ProductsInitializerUrl:Url"] ?? string.Empty;

    var productsInitializer = new ProductsInitializer(dbContext, url, logger);
    await productsInitializer.InitializeAsync();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");

app.Run();