using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _students = new();
    private readonly Dictionary<string, Group> _groups = new();

    public StubStudentRepository()
    {
        Seed();
    }

    private void Seed()
    {
        // ─────────────────────────────
        // Only ONE group (as required)
        // ─────────────────────────────
        var group = new Group("TEST", "Test group");
        _groups[group.Code] = group;

        // ─────────────────────────────
        // Only ONE student (as required)
        // ─────────────────────────────
        var student = new Student(
            999,
            "Test Student",
            "test@test.lt",
            "TEST"
        );

        student.AddGrade(10);
        student.AddGrade(10);
        student.AddGrade(10);

        _students[student.Id] = student;
    }

    // ─────────────────────────────
    // Interface implementation
    // ─────────────────────────────

    public List<Student> GetStudents()
        => _students.Values.ToList();

    public List<Group> GetGroups()
        => _groups.Values.ToList();

    public void AddStudent(Student student)
    {
        // Stub repository does NOT persist data
        throw new NotSupportedException();
    }
}