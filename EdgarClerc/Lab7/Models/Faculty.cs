using Lab7.Interfaces;

namespace Lab7.Models;

public class Faculty : IEntity
{
    public int Id { get; }
    public string Name { get; }

    private readonly List<Group> _groups = new();
    public IReadOnlyList<Group> Groups => _groups;

    public int TotalStudents => _groups.Sum(g => g.Students.Count);

    public Faculty(int id, string name)
    {
        Id = id;
        Name = name;
    }

    // Called only by Repository during initialization
    public void AddGroup(Group group) => _groups.Add(group);
}
