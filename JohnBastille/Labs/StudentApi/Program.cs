using StudentApi.Interfaces;
using StudentApi.Printers;
using StudentApi.Repositories;
using StudentApi.Services;
using StudentApi.Strategies;
using StudentApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// === DI registrations ===
builder.Services.AddSingleton<IStudentRepository, MemoryStudentRepository>();
builder.Services.AddSingleton<IStudentValidator, BasicStudentValidator>();
builder.Services.AddSingleton<IStudentPrinter, SimpleStudentPrinter>();

builder.Services.AddSingleton<IAverageStrategy>(sp =>
{
    var config = builder.Configuration["AVERAGE_STRATEGY"] ?? "simple";
    return config.ToLower() switch
    {
        "median" => new MedianAverageStrategy(),
        "weighted" => new WeightedAverageStrategy(),
        _ => new SimpleAverageStrategy()
    };
});

builder.Services.AddSingleton<StudentService>();

var app = builder.Build();

app.MapGet("/api/info", () => new
{
    service = "StudentApi",
    version = "1.0",
    status = "ok"
});

app.MapGet("/api/students", (StudentService service) =>
{
    var result = service.GetAll()
        .Select(x => new
        {
            x.student.Id,
            x.student.Name,
            x.student.Age,
            Average = x.average,
            Formatted = x.formatted
        });

    return Results.Ok(result);
});

app.MapPost("/api/students", async (StudentService service, HttpContext ctx) =>
{
    var dto = await ctx.Request.ReadFromJsonAsync<CreateStudentDto>();
    if (dto is null)
        return Results.BadRequest("Invalid body");

    var result = service.CreateStudent(dto.Name, dto.Age, dto.Grades);
    if (!result.IsSuccess)
        return Results.BadRequest(result.Error);

    var s = result.Student!;
    var avg = service
        .GetAll()
        .First(x => x.student.Id == s.Id).average;

    return Results.Ok(new
    {
        s.Id,
        s.Name,
        s.Age,
        Average = avg
    });
});

app.Run();

public record CreateStudentDto(string Name, int Age, List<int> Grades);
