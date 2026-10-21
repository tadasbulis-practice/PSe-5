using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group> _groups = new();

    public StubStudentRepository()
    {
        _groups.Add(new Group { Code = "TEST", Name = "Test group" });

        _students.Add(new Student
        {
            Id = 999,
            Name = "Test Student",
            Email = "test@test.lt",
            GroupCode = "TEST",
            Grades = new List<int> { 10, 10, 10 }
        });
    }

    public IReadOnlyList<Student> GetAllStudents() => _students;

    public IReadOnlyList<Group> GetAllGroups() => _groups;

    public Student? GetById(int id)
    {
        foreach (var s in _students)
            if (s.Id == id)
                return s;
        return null;
    }

    // Stubs typically don't persist anything — refuse writes.
    public void AddStudent(Student student) =>
        throw new NotSupportedException("StubStudentRepository is read-only.");

    public bool GroupExists(string code)
    {
        foreach (var g in _groups)
            if (g.Code == code)
                return true;
        return false;
    }
}