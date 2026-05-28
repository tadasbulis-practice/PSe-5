using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group> _groups = new();

    public MemoryStudentRepository()
    {
        _groups.Add(new Group { Code = "Pse-05", Name = "Program software english 2025" });
        _groups.Add(new Group { Code = "Ps-05", Name = "Program software 2025" });

        _students.Add(new Student
        {
            Id = 1,
            Name = "Djamel Hammar",
            Email = "djamelhammar@gmail.com",
            GroupCode = "Pse-05",
            Grades = new List<int> { 10, 9, 10, 10 }
        });
    }

    public List<Student> GetStudents() => _students;
    public List<Group> GetGroups() => _groups;
    public void Add(Student student) => _students.Add(student);

    public void UpdateStudent(Student student)
    {
        var existing = _students.FirstOrDefault(s => s.Id == student.Id);
        if (existing != null)
        {
            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.GroupCode = student.GroupCode;
            existing.Grades = student.Grades;
        }
    }
}