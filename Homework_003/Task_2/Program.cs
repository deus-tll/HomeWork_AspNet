using Microsoft.Extensions.FileProviders;
using Task_2.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js")),
        RequestPath = "/js",
        OnPrepareResponse = ctx =>
        {
            if (ctx.Context.Response.ContentType == "application/octet-stream")
            {
                ctx.Context.Response.ContentType = "text/javascript";
            }
        }
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html")),
        RequestPath = "/html"
    });

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<AuthenticationMiddleware>();
    app.UseMiddleware<RoutingMiddleware>();
}
else if (app.Environment.IsProduction())
{
    app.Run(async (context) => await context.Response.WriteAsync("In Production Stage"));
}

app.Run();