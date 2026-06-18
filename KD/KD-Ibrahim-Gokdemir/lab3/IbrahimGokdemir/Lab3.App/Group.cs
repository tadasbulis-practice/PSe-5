using System.Collections.Generic;

public class Group
{
    public string Name { get; }
    public List<Student> Students { get; }

    public Group(string name)
    {
        Name = name;
        Students = new List<Student>();
    }
}
