using System;
namespace Nassim.Lab4.Nassim.Service{
public class Group
{
    public string Name { get; set; }
    public List<Student> Students { get; set; }

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
        Console.WriteLine($"=== Group: {Name} ===");
        foreach (Student s in Students)
        {
            Console.WriteLine($"[{s.Id}] {s.FirstName} {s.LastName} | Email: {s.Email} | Average Grade: {s.AverageGrade}");
        }
    }
}
}