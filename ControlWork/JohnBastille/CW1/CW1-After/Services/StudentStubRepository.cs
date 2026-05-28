using CW1After.Interfaces;
using CW1After.Models;
using System.Collections.Generic;


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
        Grades = { 10, 10, 10 }
    }
};

    public IEnumerable<Student> GetAll() => _students;
    public Student? GetById(int id) => _students.Find(s => s.Id == id);
    public void Add(Student student) => _students.Add(student);
    public void AddGrade(int id, int grade)
    {
        var s = GetById(id);
        if (s != null) s.Grades.Add(grade);
    }
    public IEnumerable<Group> GetGroups() => _groups;
}