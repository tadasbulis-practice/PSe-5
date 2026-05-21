using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Group> _groups;
    private readonly List<Student> _students;

    public StubStudentRepository()
    {
        _groups = new List<Group>
        {
            new Group { Code = "TEST", Name = "Test group" }
        };

        _students = new List<Student>
        {
            new Student
            {
                Id = 999,
                Name = "Test Student",
                Email = "test@test.lt",
                GroupCode = "TEST",
                Grades = new List<int> { 10, 10, 10 }
            }
        };
    }

    public List<Student> GetAllStudents() => _students;

    public Student? GetStudentById(int id)
    {
        foreach (var s in _students)
            if (s.Id == id) return s;
        return null;
    }

    // Stubs typically don't persist anything
    public void AddStudent(Student student) => throw new NotSupportedException("StubStudentRepository is read-only.");
    public void AddGrade(int studentId, int grade) => throw new NotSupportedException("StubStudentRepository is read-only.");
    public void AddGroup(Group group) => throw new NotSupportedException("StubStudentRepository is read-only.");

    public List<Group> GetAllGroups() => _groups;
}
