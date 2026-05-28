using System.Collections.Generic;

public class Group
{
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new();
}