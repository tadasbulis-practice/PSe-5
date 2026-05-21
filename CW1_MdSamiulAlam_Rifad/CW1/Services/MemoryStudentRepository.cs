using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public MemoryStudentRepository()
    {
        _groups = new List<Group>
        {
            new Group { Code = "PSe-5",  Name = "Programming Studies 5" },
            new Group { Code = "IS-3",  Name = "Information Systems 3" },
            new Group { Code = "CS-2",  Name = "Computer Science 2" }
        };

        _students = new List<Student>
        {
            new Student { Id = 1, Name = "Kamal Demnati",   Email = "kamal@kk.lt",        GroupCode = "PSe-5",  Grades = new List<int> { 8, 9, 10, 7 } },
            new Student { Id = 2, Name = "Djamal Hammer",   Email = "djamal@kk.lt",       GroupCode = "PSe-5",  Grades = new List<int> { 6, 7, 5, 8 } },
            new Student { Id = 3, Name = "Samiul Rifad",    Email = "samiul@kk.lt",       GroupCode = "IS-3",  Grades = new List<int> { 10, 10, 9, 10 } },
            new Student { Id = 4, Name = "Abdul Quddus",    Email = "quddus@school.lt",   GroupCode = "CS-2",  Grades = new List<int> { 4, 5, 6, 5 } },
            new Student { Id = 5, Name = "John Bastile",    Email = "john@university.lt", GroupCode = "IS-3",  Grades = new List<int> { 9, 8, 9, 7 } }
        };
    }

    public List<Student> GetAllStudents() => _students;

    public Student? GetStudentById(int id)
    {
        foreach (var s in _students)
            if (s.Id == id) return s;
        return null;
    }

    public void AddStudent(Student student) => _students.Add(student);

    public void AddGrade(int studentId, int grade)
    {
        var student = GetStudentById(studentId);
        student?.Grades.Add(grade);
    }

    public List<Group> GetAllGroups() => _groups;

    public void AddGroup(Group group) => _groups.Add(group);
}
