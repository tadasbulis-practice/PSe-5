using System;
using System.Collections.Generic;

public class Group
{
    public string Name { get; set; } = "";
    public List<Student> Students { get; set; } = new List<Student>();

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void PrintAll()
    {
        Console.WriteLine($"Group: {Name}");
        Console.WriteLine("Students:");

        foreach (Student s in Students)
        {
            Console.WriteLine($"ID: {s.Id}");
            Console.WriteLine($"Name: {s.FirstName} {s.LastName}");
            Console.WriteLine($"Email: {s.Email}");
            Console.WriteLine($"Average Grade: {s.AverageGrade}");
            Console.WriteLine("--------------------");
        }
    }
}
