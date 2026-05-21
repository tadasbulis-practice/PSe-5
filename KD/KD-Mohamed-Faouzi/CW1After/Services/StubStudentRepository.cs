using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group> _groups = new();

    public StubStudentRepository()
    {
        _groups.Add(new Group
        {
            Code = "TEST",
            Name = "Test group"
        });

        _students.Add(new Student
        {
            Id = 999,
            Name = "Test Student",
            Email = "test@test.lt",
            GroupCode = "TEST",
            Grades = new List<int>
            {
                10, 10, 10
            }
        });
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public List<Group> GetAllGroups()
    {
        return _groups;
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(
            s => s.Id == id);
    }

    public void Add(Student student)
    {
        throw new NotSupportedException(
            "Stub repository does not support adding.");
    }
}