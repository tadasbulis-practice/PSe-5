using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations;

// Fallback / offline repository. Notice it does NOT reimplement the
// dictionary CRUD logic — it delegates that to MemoryRepository<Student>
// and only adds the Student-specific Group/Faculty lookups on top.
public class MemoryStudentRepository : IStudentRepository
{
    private readonly MemoryRepository<Student> _students = new();
    private readonly Dictionary<string, Group> _groups;
    private readonly Faculty _faculty;

    public MemoryStudentRepository()
    {
        _groups = new Dictionary<string, Group>
        {
            ["IF-21"] = new Group("IF-21", "Informatika 21", "Faculty of Informatics"),
            ["IF-22"] = new Group("IF-22", "Informatika 22", "Faculty of Informatics")
        };

        _faculty = new Faculty("Faculty of Informatics", "Kaunas College - Informatics programs");
    }

    public void Add(Student student) => _students.Add(student);

    public Student? GetById(int id) => _students.GetById(id);

    public IReadOnlyList<Student> GetAll() => _students.GetAll();

    public bool Remove(int id) => _students.Remove(id);

    public Group? GetGroupByCode(string code)
        => _groups.TryGetValue(code, out var group) ? group : null;

    public IReadOnlyList<Group> GetAllGroups() => _groups.Values.ToList();

    public Faculty GetFaculty() => _faculty;
}
