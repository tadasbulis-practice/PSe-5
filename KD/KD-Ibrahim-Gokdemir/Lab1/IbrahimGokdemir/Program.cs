using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Group group = new Group("OOP Group");

        group.AddStudent(new Student(1, "a", "a", "a@gmail.com", new List<int> { 8, 9, 10 }));
        group.AddStudent(new Student(2, "b", "b", "b@gmail.com", new List<int> { 7, 8, 9 }));
        group.AddStudent(new Student(3, "c", "c", "c@gmail.com", new List<int> { 6, 7, 8 }));

        Console.WriteLine("=== LAB 1 ===");
        group.PrintAll();

        int searchId = 2;
        Student? found = group.FindById(searchId);
        if (found != null)
        {
            Console.WriteLine($"Student Found: {found.FirstName} {found.LastName}");
        }

        Console.ReadKey();
    }
}