using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

/// <summary>
/// In-memory repository for use when the Docker API is not running.
/// Uses the same internal structure (Dictionary) as ApiStudentRepository
/// so students can see the pattern even without Docker.
/// </summary>
public class MemoryStudentRepository : StudentRepositoryBase
{
    public MemoryStudentRepository() : base("Faculty of Technology (Demo)")
    {
        // seed data — identique à avant
        var seed = new[]
        {
            new Student(1, "Alice",   "Smith",    "alice@uni.lt",   "Computer Science", 2023),
            new Student(2, "Bob",     "Jones",    "bob@uni.lt",     "Computer Science", 2023),
            new Student(3, "Charlie", "Brown",    "charlie@uni.lt", "Computer Science", 2024),
            new Student(4, "Diana",   "Lee",      "diana@uni.lt",   "Informatics",      2023),
            new Student(5, "Eve",     "Williams", "eve@uni.lt",     "Informatics",      2023),
            new Student(6, "Frank",   "Miller",   "frank@uni.lt",   "Informatics",      2024),
        };
        foreach (var s in seed) RegisterStudent(s); // ← vient de la base
    }

    // ← Supprimer BuildGroupCode, RegisterStudent, _studentsById, _groupsByCode, _faculty

    public override IReadOnlyList<Student> GetAll()          => _studentsById.Values.ToList();
    public override Student? GetById(int id)                 => _studentsById.TryGetValue(id, out var s) ? s : null;
    public override IReadOnlyList<Group> GetAllGroups()      => _groupsByCode.Values.ToList();
    public override Group? GetGroupByCode(string code)       => _groupsByCode.TryGetValue(code, out var g) ? g : null;
    public override Faculty GetFaculty()                     => _faculty;
    public override void Add(Student student)                => RegisterStudent(student);
    public override bool Remove(int id)                      => _studentsById.Remove(id);
}
}
