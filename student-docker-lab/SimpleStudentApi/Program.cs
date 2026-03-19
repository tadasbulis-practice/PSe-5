var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/api/info"));

app.MapGet("/api/info", () =>
{
    return Results.Json(new
    {
        service = "SimpleStudentApi",
        version = "1.0",
        status = "ok",
        message = "Hello from .NET 8 Minimal API",
        timestampUtc = DateTime.UtcNow,
        environment = app.Environment.EnvironmentName
    });
});

app.MapGet("/api/getStudent", async () =>
{
    var random = new Random();
    int delaySeconds = random.Next(1, 6); // 1–5 seconds

    await Task.Delay(delaySeconds * 1000);

    return Results.Json(new
    {
        firstName = "Tadas",
        LastName = "Bulis",
        status = "ok",
        delay = delaySeconds,
        message = "Response delayed intentionally",
        timestampUtc = DateTime.UtcNow
    });
});


app.Run();
