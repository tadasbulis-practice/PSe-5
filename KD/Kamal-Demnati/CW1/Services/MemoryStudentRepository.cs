using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services;

public class MemoryStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _students = new();
    private readonly Dictionary<string, Group> _groups = new();

    public MemoryStudentRepository()
    {
        Seed();
    }

    private void Seed()
    {
        // ─────────────────────────────
        // Groups (from your original CW1)
        // ─────────────────────────────
        AddGroup(new Group("PI23", "Programu inzinerija 2023"));
        AddGroup(new Group("PI24", "Programu inzinerija 2024"));

        // ─────────────────────────────
        // Students (exact CW1 data)
        // ─────────────────────────────

        AddStudentInternal(new Student(1, "Jonas Jonaitis", "jonas@kauko.lt", "PI23"));
        AddStudentInternal(new Student(2, "Greta Petraityte", "greta@kauko.lt", "PI23"));
        AddStudentInternal(new Student(3, "Mantas Kazlauskas", "mantas@kauko.lt", "PI24"));
        AddStudentInternal(new Student(4, "Ieva Andriukaityte", "ieva@kauko.lt", "PI23"));
        AddStudentInternal(new Student(5, "Tomas Bagdonas", "tomas@kauko.lt", "PI24"));

        // ─────────────────────────────
        // Grades (exact from your CW1)
        // ─────────────────────────────

        AddGrades(1, 8, 9, 7, 10);
        AddGrades(2, 6, 5, 7, 8);
        AddGrades(3, 9, 9, 10, 8);
        AddGrades(4, 10, 10, 9, 9);
        AddGrades(5, 5, 6, 6, 7);
    }

    // ─────────────────────────────
    // Internal helpers
    // ─────────────────────────────

    private void AddGroup(Group group)
    {
        _groups[group.Code] = group;
    }

    private void AddStudentInternal(Student student)
    {
        _students[student.Id] = student;
    }

    private void AddGrades(int studentId, params int[] grades)
    {
        if (_students.TryGetValue(studentId, out var student))
        {
            foreach (var g in grades)
            {
                student.AddGrade(g);
            }
        }
    }

    // ─────────────────────────────
    // Interface implementation
    // ─────────────────────────────

    public List<Student> GetStudents()
        => _students.Values.ToList();

    public List<Group> GetGroups()
        => _groups.Values.ToList();

    public void AddStudent(Student student)
        => AddStudentInternal(student);
}