using Lab7.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// =============================================================
// SAMPLE DATA — same shape MemoryStudentRepository uses,
// so flipping useApi=true in Lab7.App produces equivalent output.
// =============================================================
var faculty = new FacultyResponse
{
    Name = "Faculty of Technology",
    Groups = new List<GroupResponse>
    {
        new GroupResponse
        {
            Code = "CS-23",
            StudyProgram = "Computer Science",
            EnrollmentYear = 2023,
            Students = new List<StudentResponse>
            {
                new() { Id = 1, FirstName = "Alice", LastName = "Johnson",
                        Email = "alice@kauko.lt",  StudyProgram = "Computer Science", EnrollmentYear = 2023 },
                new() { Id = 2, FirstName = "Bob",   LastName = "Smith",
                        Email = "bob@kauko.lt",    StudyProgram = "Computer Science", EnrollmentYear = 2023 },
                new() { Id = 3, FirstName = "Carol", LastName = "Davis",
                        Email = "carol@kauko.lt",  StudyProgram = "Computer Science", EnrollmentYear = 2023 }
            }
        },
        new GroupResponse
        {
            Code = "SE-24",
            StudyProgram = "Software Engineering",
            EnrollmentYear = 2024,
            Students = new List<StudentResponse>
            {
                new() { Id = 4, FirstName = "David", LastName = "Wilson",
                        Email = "david@kauko.lt", StudyProgram = "Software Engineering", EnrollmentYear = 2024 },
                new() { Id = 5, FirstName = "Eva",   LastName = "Martinez",
                        Email = "eva@kauko.lt",   StudyProgram = "Software Engineering", EnrollmentYear = 2024 }
            }
        }
    }
};

// =============================================================
// ENDPOINTS
// =============================================================
app.MapGet("/",                  () => "Lab7.Api is running. Try /api/faculty");
app.MapGet("/api/faculty",       () => faculty);
app.MapGet("/api/groups",        () => faculty.Groups);
app.MapGet("/api/groups/{code}", (string code) =>
{
    var group = faculty.Groups.FirstOrDefault(g => g.Code == code);
    return group is null ? Results.NotFound() : Results.Ok(group);
});

// Listen on the port Lab7.App expects (6001).
app.Run("http://0.0.0.0:6001");