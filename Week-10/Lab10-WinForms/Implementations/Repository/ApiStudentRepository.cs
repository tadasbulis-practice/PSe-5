using System.Net.Http.Json;
using System.Text.Json;
using Lab10.Exceptions;
using Lab10.Interfaces;
using Lab10.Models;

namespace Lab10.Implementations.Repository;

/// <summary>
/// Loads students from the Docker REST API used in Lab-7..9.
/// All HTTP / JSON failures are wrapped as RepositoryException so callers
/// can decide how to display the error (Console.WriteLine, MessageBox, etc.).
/// </summary>
public class ApiStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _byId = new();
    private readonly HttpClient _http;

    private record ApiStudentDto(
        int Id, string FirstName, string LastName,
        string Email, string StudyProgram, int EnrollmentYear);

    private record ApiGroupDto(
        string Code, string StudyProgram, int EnrollmentYear,
        List<ApiStudentDto> Students);

    private record ApiFacultyDto(string Name, List<ApiGroupDto> Groups);

    public ApiStudentRepository(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(5) };
        try
        {
            LoadFromApi().GetAwaiter().GetResult();
        }
        catch (HttpRequestException ex)
        {
            throw new RepositoryException(
                $"Could not reach API at {baseUrl}. Is Docker running?", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new RepositoryException(
                $"API at {baseUrl} timed out.", ex);
        }
        catch (JsonException ex)
        {
            throw new RepositoryException(
                "API returned invalid JSON.", ex);
        }
    }

    private async Task LoadFromApi()
    {
        var dto = await _http.GetFromJsonAsync<ApiFacultyDto>("/api/faculty");
        if (dto is null)
            throw new RepositoryException("API returned an empty response.");

        foreach (var groupDto in dto.Groups)
        foreach (var s in groupDto.Students)
        {
            // Constructor validation runs here — bad rows surface as ArgumentException
            _byId[s.Id] = new Student(
                s.Id, s.FirstName, s.LastName,
                s.Email, s.StudyProgram, s.EnrollmentYear);
        }
    }

    public IReadOnlyList<Student> GetAll() => _byId.Values.ToList();
    public Student? GetById(int id)        => _byId.TryGetValue(id, out var s) ? s : null;
    public void     Add(Student s)         => _byId[s.Id] = s;
    public bool     Remove(int id)         => _byId.Remove(id);
}
