using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;


/// Default in-memory repository pre-loaded with the original 5 students.
/// Selected when the program is launched without --stub.

public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group>   _groups   = new();

    public MemoryStudentRepository()
    {
        _groups.Add(new Group { Code = "PI23", Name = "Programu inzinerija 2023" });
        _groups.Add(new Group { Code = "PI24", Name = "Programu inzinerija 2024" });

        _students.Add(new Student { Id = 1, Name = "Jonas Jonaitis",     Email = "jonas@kauko.lt",  GroupCode = "PI23", Grades = new List<int> { 8, 9, 7, 10 } });
        _students.Add(new Student { Id = 2, Name = "Greta Petraityte",   Email = "greta@kauko.lt",  GroupCode = "PI23", Grades = new List<int> { 6, 5, 7, 8  } });
        _students.Add(new Student { Id = 3, Name = "Mantas Kazlauskas",  Email = "mantas@kauko.lt", GroupCode = "PI24", Grades = new List<int> { 9, 9, 10, 8 } });
        _students.Add(new Student { Id = 4, Name = "Ieva Andriukaityte", Email = "ieva@kauko.lt",   GroupCode = "PI23", Grades = new List<int> { 10, 10, 9, 9 } });
        _students.Add(new Student { Id = 5, Name = "Tomas Bagdonas",     Email = "tomas@kauko.lt",  GroupCode = "PI24", Grades = new List<int> { 5, 6, 6, 7  } });
    }

    public IReadOnlyList<Student> GetAllStudents() => _students.AsReadOnly();
    public IReadOnlyList<Group>   GetAllGroups()   => _groups.AsReadOnly();
    public Student?               GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void AddStudent(Student student) => _students.Add(student);

    public void AddGrade(int studentId, int grade)
    {
        var student = GetStudentById(studentId);
        student?.Grades.Add(grade);
    }
}
