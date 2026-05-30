using Lab10.Interfaces;
using Lab10.Models;

namespace Lab10.Implementations.Repository;

/// <summary>
/// In-memory repository — fallback when API is unavailable.
/// Seeded with a mix of Students and one GraduateStudent to demonstrate
/// the inheritance hierarchy from Lab-8.
/// </summary>
public class MemoryStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _byId = new();

    public MemoryStudentRepository()
    {
        var seed = new Student[]
        {
            new Student(1, "Alice",   "Smith",    "alice@uni.lt",   "Computer Science", 2023),
            new Student(2, "Bob",     "Jones",    "bob@uni.lt",     "Computer Science", 2023),
            new Student(3, "Charlie", "Brown",    "charlie@uni.lt", "Computer Science", 2024),
            new Student(4, "Diana",   "Lee",      "diana@uni.lt",   "Informatics",      2023),
            new Student(5, "Eve",     "Williams", "eve@uni.lt",     "Informatics",      2024),
            new GraduateStudent(6, "Frank", "Miller",  "frank@uni.lt", "Informatics", 2022,
                                "Distributed cache invalidation in microservices"),
        };

        foreach (var s in seed) _byId[s.Id] = s;
    }

    public IReadOnlyList<Student> GetAll() => _byId.Values.ToList();
    public Student? GetById(int id)        => _byId.TryGetValue(id, out var s) ? s : null;
    public void     Add(Student s)         => _byId[s.Id] = s;
    public bool     Remove(int id)         => _byId.Remove(id);
}
