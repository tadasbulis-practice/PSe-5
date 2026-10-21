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

app.Run();
