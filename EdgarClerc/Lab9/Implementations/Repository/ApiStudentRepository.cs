using System.Net.Http.Json;
using System.Text.Json;
using Lab9.DTOs;
using Lab9.Exceptions;
using Lab9.Models;

namespace Lab9.Implementations.Repository;

/// <summary>
/// Fetches data from the Student REST API and organises it into an efficient
/// internal structure.
///
/// KEY OOP LESSON:
///   INSIDE this class  → complex: HttpClient, Dictionary<int,Student>,
///                                 Dictionary<string,Group>, JSON mapping
///   OUTSIDE this class → simple:  IReadOnlyList<Student>, Group, Faculty
///
/// The StudentService never knows the data came from an API,
/// never knows a Dictionary exists, and never touches any DTO.
/// </summary>
public class ApiStudentRepository : StudentRepositoryBase
{
    // ══════════════════════════════════════════════════════════════════
    // INTERNAL COMPLEXITY — private, hidden from every other class
    // ══════════════════════════════════════════════════════════════════
    private readonly Dictionary<int, Student> _studentsById = new();
    private readonly Dictionary<string, Group> _groupsByCode = new();
    private Faculty _faculty = new("Faculty of Technology");

    private readonly HttpClient _http;

    public ApiStudentRepository(string baseUrl)
        : base("Faculty of Technology")
    {
        try
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
            LoadFromApi().GetAwaiter().GetResult();
        }
        catch (HttpRequestException e)
        {
            throw new RepositoryException("Error while fetching student data from database : ", e);
        }
        catch (JsonException e)
        {
            throw new RepositoryException("Error while parsing student data from database : ", e);
        }
    }

    private async Task LoadFromApi()
    {
        Console.WriteLine($"  [Repository] Connecting to API ...");

        var dto = await _http.GetFromJsonAsync<ApiFacultyDto>("/api/faculty");
        if (dto is null)
        {
            Console.WriteLine("  [Repository] WARNING: received null from API.");
            return;
        }

        _faculty = new Faculty(dto.Name);

        foreach (var groupDto in dto.Groups)
        {
            var group = new Group(groupDto.Code, groupDto.StudyProgram, groupDto.EnrollmentYear);

            foreach (var sDto in groupDto.Students)
            {
                var student = new Student(
                    sDto.Id,
                    sDto.FirstName,
                    sDto.LastName,
                    sDto.Email,
                    sDto.StudyProgram,
                    sDto.EnrollmentYear
                );

                _studentsById[student.Id] = student; // O(1) index by ID
                group.AddStudent(student);
            }

            _groupsByCode[groupDto.Code] = group; // O(1) index by code
            _faculty.AddGroup(group);
        }

        Console.WriteLine(
            $"  [Repository] Loaded {_studentsById.Count} students "
                + $"across {_groupsByCode.Count} groups.\n"
        );
    }

    // ── Public interface — simple, clean, O(1) lookups ──────────────
    public override IReadOnlyList<Student> GetAll() => _studentsById.Values.ToList();

    public override Student? GetById(int id) => _studentsById.TryGetValue(id, out var s) ? s : null;

    public override IReadOnlyList<Group> GetAllGroups() => _groupsByCode.Values.ToList();

    public override Group? GetGroupByCode(string code) =>
        _groupsByCode.TryGetValue(code, out var g) ? g : null;

    public override Faculty GetFaculty() => _faculty;

    public override void Add(Student student) => RegisterStudent(student);

    public override bool Remove(int id)
    {
        if (!_studentsById.Remove(id))
            return false;

        //Remove in the group
        var student = GetById(id);
        if (student is null)
        {
            Console.WriteLine("Student not found");
            return false;
        }

        var groupWithStudent = _groupsByCode.Where(g => g.Value.Students.Contains(student));
        foreach (var group in groupWithStudent)
        {
            group.Value.RemoveStudent(student);
        }

        return true;
    }
}
