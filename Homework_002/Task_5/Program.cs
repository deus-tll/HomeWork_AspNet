using Microsoft.Extensions.FileProviders;
using Task_5.controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js")),
    RequestPath = "/js",
    OnPrepareResponse = ctx =>
    {
        if(ctx.Context.Response.ContentType == "application/octet-stream")
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

app.MapControllers();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(Path.Combine("wwwroot", "html", "index.html"));
});

app.Run();