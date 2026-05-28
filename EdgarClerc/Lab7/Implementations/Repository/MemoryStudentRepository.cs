using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

/// <summary>
/// In-memory repository for use when the Docker API is not running.
/// Uses the same internal structure (Dictionary) as ApiStudentRepository
/// so students can see the pattern even without Docker.
/// </summary>
public class MemoryStudentRepository : IStudentRepository
{
    // ── Internal data structures (hidden from all callers) ─────────
    private readonly Dictionary<int, Student> _studentsById = new();
    private readonly Dictionary<string, Group> _groupsByCode = new();
    private readonly Faculty _faculty;

    public MemoryStudentRepository()
    {
        _faculty = new Faculty(1, "Faculty of Technology (Demo)");

        // Seed data that mirrors what the real API would return
        var seed = new[]
        {
            new Student(1, "Alice", "Smith", "alice@uni.lt", "Computer Science", 2023),
            new Student(2, "Bob", "Jones", "bob@uni.lt", "Computer Science", 2023),
            new Student(3, "Charlie", "Brown", "charlie@uni.lt", "Computer Science", 2024),
            new Student(4, "Diana", "Lee", "diana@uni.lt", "Informatics", 2023),
            new Student(5, "Eve", "Williams", "eve@uni.lt", "Informatics", 2023),
            new Student(6, "Frank", "Miller", "frank@uni.lt", "Informatics", 2024),
        };

        foreach (var s in seed)
            RegisterStudent(s);
    }

    // ── Private helpers ─────────────────────────────────────────────
    private void RegisterStudent(Student student)
    {
        _studentsById[student.Id] = student;

        var code = BuildGroupCode(student.StudyProgram, student.EnrollmentYear);
        if (!_groupsByCode.TryGetValue(code, out var group))
        {
            group = new Group(1, code, student.StudyProgram, student.EnrollmentYear);
            _groupsByCode[code] = group;
            _faculty.AddGroup(group);
        }
        group.AddStudent(student);
    }

    private static string BuildGroupCode(string studyProgram, int year)
    {
        var words = studyProgram.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var prefix = string.Concat(words.Select(w => char.ToUpper(w[0])));
        return $"{prefix}-{year % 100:D2}";
    }

    // ── Public interface — simple, clean, O(1) lookups ──────────────
    public IReadOnlyList<Student> GetAll() => _studentsById.Values.ToList();

    public Student? GetById(int id) => _studentsById.TryGetValue(id, out var s) ? s : null;

    public IReadOnlyList<Group> GetAllGroups() => _groupsByCode.Values.ToList();

    public Group? GetGroupByCode(string code) =>
        _groupsByCode.TryGetValue(code, out var g) ? g : null;

    public Faculty GetFaculty() => _faculty;

    public void Add(Student student) => RegisterStudent(student);

    public bool Remove(int id)
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
