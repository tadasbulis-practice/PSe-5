using System.Net.Http.Json;
using Lab7.DTOs;
using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

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
public class ApiStudentRepository : IStudentRepository
{
    // ══════════════════════════════════════════════════════════════════
    // INTERNAL COMPLEXITY — private, hidden from every other class
    // ══════════════════════════════════════════════════════════════════
    private readonly Dictionary<int, Student> _studentsById = new();
    private readonly Dictionary<string, Group> _groupsByCode = new();
    private Faculty _faculty = new(1, "Faculty of Technology");

    private readonly HttpClient _http;

    public ApiStudentRepository(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        LoadFromApi().GetAwaiter().GetResult();
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

        _faculty = new Faculty(1, dto.Name);

        foreach (var groupDto in dto.Groups)
        {
            var group = new Group(1, groupDto.Code, groupDto.StudyProgram, groupDto.EnrollmentYear);

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

    // ══════════════════════════════════════════════════════════════════
    // PUBLIC INTERFACE — clean, simple, no API or Dictionary visible
    // ══════════════════════════════════════════════════════════════════
    public IReadOnlyList<Student> GetAll() => _studentsById.Values.ToList();

    public Student? GetById(int id) => _studentsById.TryGetValue(id, out var s) ? s : null;

    public IReadOnlyList<Group> GetAllGroups() => _groupsByCode.Values.ToList();

    public Group? GetGroupByCode(string code) =>
        _groupsByCode.TryGetValue(code, out var g) ? g : null;

    public Faculty GetFaculty() => _faculty;

    public void Add(Student student) => _studentsById[student.Id] = student;

    public bool Remove(int id) => _studentsById.Remove(id);
}
