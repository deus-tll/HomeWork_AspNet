using InternetShop.Filters;
using InternetShop.Models;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.InitializeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(MEAsyncFilterAttribute));
    options.Filters.Add(typeof(LastVisitCookieFilter));
});


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddAuthentication("MyCookieAuthenticationScheme").AddCookie("MyCookieAuthenticationScheme", options =>
{
    options.LoginPath = new PathString("/Home/Account/Login");
    options.LogoutPath = new PathString("/Home/Account/Logout");
});


builder.Services.AddScoped<IUserHandler, UserHandler>();
builder.Services.AddScoped<IProductHandler, ProductHandler>();
builder.Services.AddScoped<LogActionFilterAttribute>();
builder.Services.AddScoped<MyExceptionFilter>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


const string dirLogsPath = "Logs";
const string fileLogs = "app.log";

var logsFolder = Path.Combine(Directory.GetCurrentDirectory(), dirLogsPath);
if (!Directory.Exists(logsFolder))
    Directory.CreateDirectory(logsFolder);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()//цією властивістю ми задаємо тип логів. поставив Information, щоб було наглядно,
                               //але це занадто багато, тому потім можна змінити, щоб тільки помилки логувались
    .WriteTo.File(Path.Combine(dirLogsPath, fileLogs))
    .CreateLogger();

//якщо залишити цю команду, то записи будуть відбуватися тільки в файл який ми вказали.
//інакше будуть працювати усі інші логери, наприклад в консоль серверу
//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationContext>();
    await dbContext.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var config = services.GetRequiredService<IConfiguration>();

    var userInitializer = new UserInitializer(userManager, roleManager, config);
    await userInitializer.InitializeAsync();

    var productInitializer = new ProductInitializer(dbContext);
    await productInitializer.InitializeAsync();
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