using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string task = "На веб-сторінці, при перезавантаженні, відображайте у випадковому порядку будь-яку букву із англійського алфавіту.";
string alphabet = "qwertyuiopasdfghjklzxcvbnm";

app.Run(async (context) =>
{
    StringBuilder sb = new();
    sb.Append("<h1>Завдання 2</h1>");
    sb.Append($"<p>{task}</p><br>");
    sb.Append($"<p>Випадкова буква: {GetRandomSymbol(alphabet)}</p>");

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(sb.ToString());
});

app.Run();

static char GetRandomSymbol(string alphabet)
{
    return alphabet[new Random().Next(alphabet.Length)];
}