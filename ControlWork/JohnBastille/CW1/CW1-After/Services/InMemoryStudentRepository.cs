using CW1After.Interfaces;
using CW1After.Models;

using System.Collections.Generic;
using System.Linq;

namespace CW1After.Services;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Group> _groups = new();
    private readonly List<Student> _students = new();

    public InMemoryStudentRepository()
    {
        _groups.Add(new Group { Code = "PI23", Name = "Programu inzinerija 2023" });
        _groups.Add(new Group { Code = "PI24", Name = "Programu inzinerija 2024" });

        _students.Add(new Student { Id = 1, Name = "Jonas Jonaitis", Email = "jonas@kauko.lt", GroupCode = "PI23", Grades = { 8, 9, 7, 10 } });
        _students.Add(new Student { Id = 2, Name = "Greta Petraityte", Email = "greta@kauko.lt", GroupCode = "PI23", Grades = { 6, 5, 7, 8 } });
        _students.Add(new Student { Id = 3, Name = "Mantas Kazlauskas", Email = "mantas@kauko.lt", GroupCode = "PI24", Grades = { 9, 9, 10, 8 } });
        _students.Add(new Student { Id = 4, Name = "Ieva Andriukaityte", Email = "ieva@kauko.lt", GroupCode = "PI23", Grades = { 10, 10, 9, 9 } });
        _students.Add(new Student { Id = 5, Name = "Tomas Bagdonas", Email = "tomas@kauko.lt", GroupCode = "PI24", Grades = { 5, 6, 6, 7 } });
    }

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student) => _students.Add(student);

    public void AddGrade(int id, int grade)
    {
        var s = GetById(id);
        if (s != null) s.Grades.Add(grade);
    }

    public IEnumerable<Group> GetGroups() => _groups;
}