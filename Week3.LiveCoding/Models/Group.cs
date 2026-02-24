namespace Week3.LiveCoding.Models;

public class Group
{
    public string Name { get; }
    public List<Student> Students { get; } = new();

    public Group(string name)
    {
        Name = name;
    }
}
