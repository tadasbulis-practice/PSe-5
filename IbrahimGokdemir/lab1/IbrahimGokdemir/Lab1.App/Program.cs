using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Group group = new Group("OOP Group");

        IGroupService service = new GroupService();

        service.RegisterStudent(group, new Student(1, "Ali", "Yilmaz", "ali@mail.com", new List<int> { 8, 9, 10 }));
        service.RegisterStudent(group, new Student(2, "Ayse", "Demir", "ayse@mail.com", new List<int> { 7, 8, 9 }));
        service.RegisterStudent(group, new Student(3, "Mehmet", "Kaya", "mehmet@mail.com", new List<int> { 6, 7, 8 }));

        IStudentPrinter printer1 = new ConsoleStudentPrinter();
        IAverageStrategy avg1 = new SimpleAverageStrategy();

        Console.WriteLine("=== Console Printer + Simple Average ===");
        printer1.Print(group);

        Console.WriteLine("\nAverages (Simple):");
        foreach (var student in service.GetAllStudents(group))
        {
            Console.WriteLine($"{student.FirstName}: {avg1.Calculate(student):0.00}");
        }

        IStudentPrinter printer2 = new CompactStudentPrinter();
        IAverageStrategy avg2 = new DropLowestAverageStrategy();

        Console.WriteLine("\n=== Compact Printer + Drop Lowest Average ===");
        printer2.Print(group);

        Console.WriteLine("\nAverages (Drop Lowest):");
        foreach (var student in service.GetAllStudents(group))
        {
            Console.WriteLine($"{student.FirstName}: {avg2.Calculate(student):0.00}");
        }

        Console.ReadKey();
    }
}