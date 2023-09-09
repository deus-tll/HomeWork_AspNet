using InternetShop.Filters;
using InternetShop.Models;
using InternetShop.Models.DataModels;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.InitializeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(MEAsyncFilterAttribute));
    options.Filters.Add(typeof(LastVisitCookieFilter));
});

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication("MyCookieAuthenticationScheme").AddCookie("MyCookieAuthenticationScheme", options =>
{
    options.LoginPath = new PathString("/Home/Account/Login");
    options.LogoutPath = new PathString("/Home/Account/Logout");
});

builder.Services.AddScoped<IUserHandler, UserHandler>();
builder.Services.AddScoped<LogActionFilterAttribute>();
builder.Services.AddScoped<MyExceptionFilter>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationContext>();
    await dbContext.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var config = services.GetRequiredService<IConfiguration>();

    var userInitializer = new UserInitializer(userManager, roleManager, config);
    await userInitializer.InitializeAsync();

    var productInitializer = new ProductInitializer(dbContext);
    await productInitializer.InitializeAsync();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();


app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");

app.Run();