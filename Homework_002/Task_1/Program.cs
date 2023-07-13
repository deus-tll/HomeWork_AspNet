var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string task = "На веб-сторінці виведіть номер поточного дня року.";

//app.MapGet("/", () => DateTime.Now.DayOfYear);
//app.Run(async (context) => await context.Response.WriteAsync(DateTime.Now.DayOfYear.ToString()));
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<h1>Завдання 1</h1><p>{task}</p><br><p>Відповідь: {DateTime.Now.DayOfYear}</p>");
});

app.Run();