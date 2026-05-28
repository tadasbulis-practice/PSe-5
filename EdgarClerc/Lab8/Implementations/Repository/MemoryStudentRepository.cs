using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Repository;

/// <summary>
/// In-memory repository for use when the Docker API is not running.
/// Uses the same internal structure (Dictionary) as ApiStudentRepository
/// so students can see the pattern even without Docker.
/// </summary>
public class MemoryStudentRepository : StudentRepositoryBase
{
    public MemoryStudentRepository()
        : base("Faculty of Technology (Demo)")
    {
        Faculty = new Faculty("Faculty of Technology (Demo)");

        // Seed data that mirrors what the real API would return
        var seed = new[]
        {
            new Student(1, "Alice", "Smith", "alice@uni.lt", "Computer Science", 2023),
            new Student(2, "Bob", "Jones", "bob@uni.lt", "Computer Science", 2023),
            new Student(3, "Charlie", "Brown", "charlie@uni.lt", "Computer Science", 2024),
            new Student(4, "Diana", "Lee", "diana@uni.lt", "Informatics", 2023),
            new Student(5, "Eve", "Williams", "eve@uni.lt", "Informatics", 2023),
            new Student(6, "Frank", "Miller", "frank@uni.lt", "Informatics", 2024),
        };

        foreach (var s in seed)
            RegisterStudent(s);
    }

    // ── Public interface — simple, clean, O(1) lookups ──────────────
    public override IReadOnlyList<Student> GetAll() => StudentsById.Values.ToList();

    public override Student? GetById(int id) => StudentsById.TryGetValue(id, out var s) ? s : null;

    public override IReadOnlyList<Group> GetAllGroups() => GroupsByCode.Values.ToList();

    public override Group? GetGroupByCode(string code) =>
        GroupsByCode.TryGetValue(code, out var g) ? g : null;

    public override Faculty GetFaculty() => Faculty;

    public override void Add(Student student) => RegisterStudent(student);

    public override bool Remove(int id)
    {
        if (!StudentsById.Remove(id))
            return false;

        //Remove in the group
        var student = GetById(id);
        if (student is null)
        {
            Console.WriteLine("Student not found");
            return false;
        }

        var groupWithStudent = GroupsByCode.Where(g => g.Value.Students.Contains(student));
        foreach (var group in groupWithStudent)
        {
            group.Value.RemoveStudent(student);
        }

        return true;
    }
}
