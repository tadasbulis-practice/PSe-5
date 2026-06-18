using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Group group = new Group("OOP Group");
        GroupService service = new GroupService();

        service.RegisterStudent(group, new Student(1, "a", "a", "a@gmail.com", new List<int> { 8, 9, 10 }));
        service.RegisterStudent(group, new Student(2, "b", "b", "b@gmail.com", new List<int> { 7, 8, 9 }));
        service.RegisterStudent(group, new Student(3, "c", "c", "c@gmail.com", new List<int> { 6, 7, 8 }));

        Console.WriteLine("=== LAB 2 ===");
        foreach (var student in service.GetAllStudents(group))
        {
            Console.WriteLine(student.Describe());
            Console.WriteLine($"Average: {student.GetAverage():0.00}");
            Console.WriteLine("-----------------------------------");
        }

        Console.WriteLine("\n=== LAB 2 Search ===");
        Student? found = service.FindById(group, 2);
        if (found != null)
        {
            Console.WriteLine($"Found: {found.Describe()}");
        }

        Console.ReadKey();
    }
}