using System.Collections.Generic;

namespace Lab4Demo.Models;

public class Group
{
    public List<Student> Students { get; } = new();

    public void Add(Student s)
    {
        Students.Add(s);
    }
}
