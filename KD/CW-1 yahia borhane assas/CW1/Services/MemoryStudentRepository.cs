using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class MemoryStudentRepository : IStudentRepository
{
    private List<Student> _students = new()
    {
        new Student
        {
            Id = 1,
            Name = "Jonas Jonaitis",
            Email = "jonas@kauko.lt",
            GroupCode = "PI23",
            Grades = new List<int> { 8, 9, 7, 10 }
        },

        new Student
        {
            Id = 2,
            Name = "Greta Petraityte",
            Email = "greta@kauko.lt",
            GroupCode = "PI23",
            Grades = new List<int> { 6, 5, 7, 8 }
        },

        new Student
        {
            Id = 3,
            Name = "Mantas Kazlauskas",
            Email = "mantas@kauko.lt",
            GroupCode = "PI24",
            Grades = new List<int> { 9, 9, 10, 8 }
        },

        new Student
        {
            Id = 4,
            Name = "Ieva Andriukaityte",
            Email = "ieva@kauko.lt",
            GroupCode = "PI23",
            Grades = new List<int> { 10, 10, 9, 9 }
        },

        new Student
        {
            Id = 5,
            Name = "Tomas Bagdonas",
            Email = "tomas@kauko.lt",
            GroupCode = "PI24",
            Grades = new List<int> { 5, 6, 6, 7 }
        }
    };

    private List<Group> _groups = new()
    {
        new Group
        {
            Code = "PI23",
            Name = "Programming Group PI23"
        },

        new Group
        {
            Code = "PI24",
            Name = "Programming Group PI24"
        }
    };

    public List<Student> GetStudents()
    {
        return _students;
    }

    public List<Group> GetGroups()
    {
        return _groups;
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }
}