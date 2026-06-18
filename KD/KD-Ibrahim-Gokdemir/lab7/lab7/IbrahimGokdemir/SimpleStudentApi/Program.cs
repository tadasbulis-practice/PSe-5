var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// --- Lab 7 addition: in-memory sample data the API serves up ---
// (Same shape as Lab7.App's domain models, just kept as simple records here
// since this is a separate, independently-deployed project.)
var students = new List<StudentDto>
{
    new(1, "Ali", "ali@test.com", "IF-21", new List<int> { 8, 9, 10 }),
    new(2, "Ayse", "ayse@test.com", "IF-21", new List<int> { 7, 6, 8 }),
    new(3, "Mehmet", "mehmet@test.com", "IF-22", new List<int> { 10, 10, 9 })
};

var groups = new List<GroupDto>
{
    new("IF-21", "Informatika 21", "Faculty of Informatics"),
    new("IF-22", "Informatika 22", "Faculty of Informatics")
};

var faculty = new FacultyDto("Faculty of Informatics", "Kaunas College - Informatics programs");

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

// --- Lab 7 endpoints: consumed by Lab7.App's ApiStudentRepository ---

app.MapGet("/api/students", () => Results.Json(students));

app.MapGet("/api/students/{id:int}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    return student is null ? Results.NotFound() : Results.Json(student);
});

app.MapPost("/api/students", (StudentDto student) =>
{
    students.Add(student);
    return Results.Created($"/api/students/{student.Id}", student);
});

app.MapDelete("/api/students/{id:int}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student is null) return Results.NotFound();
    students.Remove(student);
    return Results.NoContent();
});

app.MapGet("/api/groups", () => Results.Json(groups));

app.MapGet("/api/groups/{code}", (string code) =>
{
    var group = groups.FirstOrDefault(g => g.Code == code);
    return group is null ? Results.NotFound() : Results.Json(group);
});

app.MapGet("/api/faculty", () => Results.Json(faculty));

app.Run();

record StudentDto(int Id, string Name, string Email, string? GroupCode, List<int> Grades);
record GroupDto(string Code, string Name, string FacultyName);
record FacultyDto(string Name, string Description);
