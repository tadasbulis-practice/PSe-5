using CW1Friend.Interfaces;
using CW1Friend.Models;

namespace CW1Friend.Services;

public class StubStudentRepository : IStudentRepository
{
    private List<Student> _students;
    private List<Group> _groups;

    public StubStudentRepository()
    {
        _groups = new List<Group>
        {
            new Group { Code = "TEST", GroupName = "Test group" }
        };

        _students = new List<Student>
        {
            new Student
            {
                Id           = 999,
                FullName     = "Test Student",
                EmailAddress = "test@test.lt",
                GroupCode    = "TEST",
                Grades       = new List<int> { 10, 10, 10 }
            }
        };
    }

    public List<Student> FetchAllStudents() => _students;

    public Student? FetchStudentById(int id)
    {
        for (int i = 0; i < _students.Count; i++)
            if (_students[i].Id == id) return _students[i];
        return null;
    }

    public void SaveStudent(Student s)   => throw new NotSupportedException("Stub repo does not support writes.");
    public void SaveGrade(int id, int g) => throw new NotSupportedException("Stub repo does not support writes.");
    public void SaveGroup(Group g)       => throw new NotSupportedException("Stub repo does not support writes.");

    public List<Group> FetchAllGroups() => _groups;
}
