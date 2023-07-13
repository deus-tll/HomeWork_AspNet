using Microsoft.Extensions.FileProviders;
using Task_3.models;

//ТРЕТЄ ТА ЧЕТВЕРТЕ ЗАВДАННЯ ОБ'ЄДАННІ ОДРАЗУ В ОДНОМУ

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string desc1 = "Заклад  поділений на три зали. Перший демонструє " +
    "відвідувачам унікальний вітраж видатного монументального живописця " +
    "Яна Генріка Розена, що розписував у Львові Вірменський собор, костел " +
    "Стрітення монастиря Кармеліток та ін. Другий – це знайомство з алкогольною " +
    "історією Львова. Третій – свого роду галерея відомих відвідувачів закладу. " +
    "«Їжа у «Mons Pius» – особлива. Авторська кухня, незмінний смак та якість улюблених " +
    "страв вирізняють її з-поміж решти. До чесних стейків з печі хоспер Вам порадять " +
    "найкраще підібрані вина або фірмове живе пиво «Mons Pius, - так відгукуються про своє меню власники закладу.";
string address1 = "Львів, вул. Л. Українки, 14.";

string desc2 = "Це заклад із історією у понад 25 років. Своїм відвідувачам «Старий рояль» " +
    "пропонує українську та європейську кухню. Також тут можна почути живу музику. У " +
    "ресторані можна ласувати стравами і дивитися просто на небо. У 2015 році після " +
    "ремонту тут зробили скляний купол на стелі.";
string address2 = "Львів, вул. Ставропігійська, 3.";

List<Restaurant> restaurants = new()
{
    new() { Id = 1, Name = "Mons Pius", Description = desc1, Address = address1, Rating = 4.5F},
    new() { Id = 2, Name = "Старий рояль", Description = desc2, Address = address2, Rating = 4.7F}
};
Restaurant? selectedRestaurant = null;

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js")),
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
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "html")),
        RequestPath = "/html"
});


app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    if (path == "/api/restaurants" && request.Method == "GET")
    {
        await GetAllRestaurants(response);
    }
    else if(path == "/" && request.Method == "GET")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync(Path.Combine("wwwroot", "html", "index.html"));
    }
    else if (path == "/api/restaurant" && request.Method == "GET")
    {
        string? stringId = request.Query["id"];
        int? id = null;
        if (stringId != null) id = int.Parse(stringId);

        selectedRestaurant = restaurants.FirstOrDefault((r) => r.Id == id);

        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync(Path.Combine("wwwroot", "html", "restaurant.html"));
    }
    else if (path == "/api/get_restaurant" && request.Method == "GET")
    {
        if (selectedRestaurant == null)
        {
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { message = "Ресторан не був знайдений." });
            return;
        }

        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(selectedRestaurant);
    }
});

app.Run();

async Task GetAllRestaurants(HttpResponse response)
{
    await response.WriteAsJsonAsync(restaurants);
}