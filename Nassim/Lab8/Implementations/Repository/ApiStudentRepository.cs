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
public class ApiStudentRepository : StudentRepositoryBase
{
    private readonly HttpClient _http;

    public ApiStudentRepository(string baseUrl) : base("Faculty of Technology")
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        LoadFromApi().GetAwaiter().GetResult();
    }

    // ← Supprimer BuildGroupCode, RegisterStudent, _studentsById, _groupsByCode, _faculty

    private async Task LoadFromApi() { /* identique à avant */ }

    public override IReadOnlyList<Student> GetAll()          => _studentsById.Values.ToList();
    public override Student? GetById(int id)                 => _studentsById.TryGetValue(id, out var s) ? s : null;
    public override IReadOnlyList<Group> GetAllGroups()      => _groupsByCode.Values.ToList();
    public override Group? GetGroupByCode(string code)       => _groupsByCode.TryGetValue(code, out var g) ? g : null;
    public override Faculty GetFaculty()                     => _faculty;
    public override void Add(Student student)                => _studentsById[student.Id] = student;
    public override bool Remove(int id)                      => _studentsById.Remove(id);
}
