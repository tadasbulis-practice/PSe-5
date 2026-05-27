namespace Lab7.Models;

public class Faculty
{
    public string Name { get; }

    private readonly List<Group> _groups = new();
    public IReadOnlyList<Group> Groups => _groups;

    public int TotalStudents => _groups.Sum(g => g.Students.Count);

    public Faculty(string name) => Name = name;

    // Called only by Repository during initialization
    public void AddGroup(Group group) => _groups.Add(group);
}
