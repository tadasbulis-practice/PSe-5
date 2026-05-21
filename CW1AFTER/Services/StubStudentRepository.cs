using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public StubStudentRepository()
    {
        _groups = new List<Group> { new Group { Code = "TEST", Name = "Test group" } };
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

    public List<Student> GetStudents() => _students;
    public List<Group> GetGroups() => _groups;
    public void Add(Student student) => throw new NotSupportedException();
    public void UpdateStudent(Student student) => throw new NotSupportedException();
}