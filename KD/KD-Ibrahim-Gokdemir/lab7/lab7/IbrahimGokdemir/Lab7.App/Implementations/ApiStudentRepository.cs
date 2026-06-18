using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations;

// Real implementation, talks to SimpleStudentApi (the Docker REST API
// from the StudentDockerLab project). Same public contract as
// MemoryStudentRepository — StudentService never knows the difference.
public class ApiStudentRepository : IStudentRepository
{
    // The API may use camelCase (ASP.NET Core's default) while our DTOs
    // are PascalCase — case-insensitive matching avoids that mismatch.
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;
    private readonly Dictionary<int, Student> _students = new();
    private readonly Dictionary<string, Group> _groups = new();
    private Faculty _faculty = new("Unknown", "Not loaded yet");
    private bool _loaded;

    public ApiStudentRepository(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    // Loads everything from the API once, then serves from memory.
    // Mirrors LoadFromApi() from the slide-29 diagram.
    private void EnsureLoaded()
    {
        if (_loaded) return;
        LoadFromApi();
        _loaded = true;
    }

    private void LoadFromApi()
    {
        var studentDtos = _http
            .GetFromJsonAsync<List<ApiStudentDto>>("/api/students", JsonOptions)
            .GetAwaiter().GetResult();

        if (studentDtos != null)
        {
            foreach (var dto in studentDtos)
            {
                var student = new Student(dto.Id, dto.Name, dto.Email, dto.Grades, dto.GroupCode);
                _students[student.Id] = student;
            }
        }

        var groupDtos = _http
            .GetFromJsonAsync<List<ApiGroupDto>>("/api/groups", JsonOptions)
            .GetAwaiter().GetResult();

        if (groupDtos != null)
        {
            foreach (var dto in groupDtos)
            {
                _groups[dto.Code] = new Group(dto.Code, dto.Name, dto.FacultyName);
            }
        }

        var facultyDto = _http
            .GetFromJsonAsync<ApiFacultyDto>("/api/faculty", JsonOptions)
            .GetAwaiter().GetResult();

        if (facultyDto != null)
        {
            _faculty = new Faculty(facultyDto.Name, facultyDto.Description);
        }
    }

    public void Add(Student student)
    {
        EnsureLoaded();
        // NOTE: SimpleStudentApi does have a POST /api/students endpoint,
        // but we only update the local cache here to keep this method
        // synchronous and matching IRepository<T>. Extend this with a
        // real POST call if write-through to the API is required.
        _students[student.Id] = student;
    }

    public Student? GetById(int id)
    {
        EnsureLoaded();
        return _students.TryGetValue(id, out var student) ? student : null;
    }

    public IReadOnlyList<Student> GetAll()
    {
        EnsureLoaded();
        return _students.Values.ToList();
    }

    public bool Remove(int id)
    {
        EnsureLoaded();
        return _students.Remove(id);
    }

    public Group? GetGroupByCode(string code)
    {
        EnsureLoaded();
        return _groups.TryGetValue(code, out var group) ? group : null;
    }

    public IReadOnlyList<Group> GetAllGroups()
    {
        EnsureLoaded();
        return _groups.Values.ToList();
    }

    public Faculty GetFaculty()
    {
        EnsureLoaded();
        return _faculty;
    }
}

// DTOs — shape of the JSON returned by SimpleStudentApi.
// Kept private to this file/class on purpose: nobody outside this
// repository needs to know the wire format.
internal class ApiStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string? GroupCode { get; set; }
    public List<int> Grades { get; set; } = new();
}

internal class ApiGroupDto
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public string FacultyName { get; set; } = "";
}

internal class ApiFacultyDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}
