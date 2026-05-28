using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StubStudentRepository : IStudentRepository
{
    public List<Student> GetStudents()
    {
        return new List<Student>
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

    public List<Group> GetGroups()
    {
        return new List<Group>
        {
            new Group
            {
                Code = "TEST",
                Name = "Test group"
            }
        };
    }

    public void Add(Student student)
    {
        throw new NotSupportedException();
    }
}