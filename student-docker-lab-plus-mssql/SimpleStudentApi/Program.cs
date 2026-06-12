using Microsoft.EntityFrameworkCore;
using SimpleStudentApi.Data;
using SimpleStudentApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Auto-create DB on startup (with retry for slow MSSQL boot)
using (var scope = app.Services.CreateScope())
{
    var maxRetries = 10;
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            Console.WriteLine("Database ready.");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DB not ready yet (attempt {i + 1}/{maxRetries}): {ex.Message}");
            if (i == maxRetries - 1) throw;
            Thread.Sleep(3000);
        }
    }
}

// ── Info ──────────────────────────────────────────────────────────────────────
app.MapGet("/", () => Results.Redirect("/api/info"));

app.MapGet("/api/info", () => Results.Json(new
{
    service    = "SimpleStudentApi",
    version    = "1.1",
    status     = "ok",
    endpoints  = new[] { "/api/students", "/api/students/{id}", "/api/faculty" },
    timestampUtc = DateTime.UtcNow
}));

// ── Students CRUD ─────────────────────────────────────────────────────────────
app.MapGet("/api/students", async (AppDbContext db) =>
    Results.Ok(await db.Students.ToListAsync()));

app.MapGet("/api/students/{id:int}", async (int id, AppDbContext db) =>
{
    var student = await db.Students.FindAsync(id);
    return student is null ? Results.NotFound() : Results.Ok(student);
});

app.MapPost("/api/students", async (Student student, AppDbContext db) =>
{
    db.Students.Add(student);
    await db.SaveChangesAsync();
    return Results.Created($"/api/students/{student.Id}", student);
});

app.MapPut("/api/students/{id:int}", async (int id, Student updated, AppDbContext db) =>
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();

    student.FirstName      = updated.FirstName;
    student.LastName       = updated.LastName;
    student.Email          = updated.Email;
    student.StudyProgram   = updated.StudyProgram;
    student.EnrollmentYear = updated.EnrollmentYear;

    await db.SaveChangesAsync();
    return Results.Ok(student);
});

app.MapDelete("/api/students/{id:int}", async (int id, AppDbContext db) =>
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();

    db.Students.Remove(student);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ── Faculty hierarchy  ────────────────────────────────────────────────────────
// NEW ENDPOINT: returns Faculty → Groups → Students
// Groups are derived by grouping students on StudyProgram + EnrollmentYear.
// This is the endpoint that Lab-7's ApiStudentRepository calls.

app.MapGet("/api/faculty", async (AppDbContext db) =>
{
    var students = await db.Students.ToListAsync();

    var groups = students
        .GroupBy(s => new { s.StudyProgram, s.EnrollmentYear })
        .Select(g => new GroupResponse
        {
            Code           = BuildGroupCode(g.Key.StudyProgram, g.Key.EnrollmentYear),
            StudyProgram   = g.Key.StudyProgram,
            EnrollmentYear = g.Key.EnrollmentYear,
            Students       = g.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList()
        })
        .OrderBy(g => g.StudyProgram)
        .ThenBy(g => g.EnrollmentYear)
        .ToList();

    var faculty = new FacultyResponse
    {
        Name   = "Faculty of Technology",
        Groups = groups
    };

    return Results.Ok(faculty);
});

// ── Helper ────────────────────────────────────────────────────────────────────
static string BuildGroupCode(string studyProgram, int year)
{
    var words  = studyProgram.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var prefix = string.Concat(words.Select(w => char.ToUpper(w[0])));
    return $"{prefix}-{year % 100:D2}";
}

app.Run();
