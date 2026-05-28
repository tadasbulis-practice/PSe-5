using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Group> _groups = new()
    {
        new Group { Code = "TEST", Name = "Test group" }
    };

    private readonly List<Student> _students = new()
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

    public List<Student> GetStudents() => _students;

    public List<Group> GetGroups() => _groups;

    public Student? GetStudentById(int id)
    {
        foreach (var student in _students)
        {
            if (student.Id == id)
            {
                return student;
            }
        }

        return null;
    }

    public void AddStudent(Student student)
    {
        throw new NotSupportedException("Stub repository does not persist new students.");
    }

    public void AddGrade(int studentId, int grade)
    {
        throw new NotSupportedException("Stub repository does not persist grades.");
    }
}
