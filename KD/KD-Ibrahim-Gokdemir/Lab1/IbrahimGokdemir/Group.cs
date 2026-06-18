using System.Collections.Generic;
using System.Linq;

public class Group
{
    public string Name { get; }
    public List<Student> Students { get; }

    public Group(string name)
    {
        Name = name;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void PrintAll()
    {
        foreach (var student in Students)
        {
            student.PrintInfo();
        }
    }

    public Student? FindById(int id)
    {
        return Students.FirstOrDefault(s => s.Id == id);
    }
}