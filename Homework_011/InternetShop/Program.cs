using InternetShop.Models;
using InternetShop.Models.HandlerModels;
using InternetShop.Models.InitializeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddAuthentication("MyCookieAuthenticationScheme").AddCookie("MyCookieAuthenticationScheme", options =>
{
    options.LoginPath = new PathString("/Home/Account/Login");
    options.LogoutPath = new PathString("/Home/Account/Logout");
});

builder.Services.AddScoped<IUserHandler, UserHandler>();

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
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();


app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{action}",
    defaults: new { controller = "Home", action = "Index" });

//(domen)/api/Home/AboutProduct/8
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}/{id}");

//(domen)/user/write_message
app.MapControllerRoute(
    name: "write_message",
    pattern: "user/write_message",
    defaults: new { controller = "Home", action = "WriteMessage" });

app.Run();