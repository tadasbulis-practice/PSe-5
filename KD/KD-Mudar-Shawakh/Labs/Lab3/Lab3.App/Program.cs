using System;
using Lab3.App.Models;
using Lab3.App.Services;
using Lab3.App.Strategies;

namespace Lab3.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Program works exclusively with interfaces
            IStudentService service = new StudentService();

            var s1 = new Student("Student 1");
            service.AddGrade(s1, 75);
            service.AddGrade(s1, 80);
            service.AddGrade(s1, 90);
            service.AddGrade(s1, 65);

            // Clear polymorphism demonstration
            IAverageStrategy simpleStrategy = new SimpleAverageStrategy();
            IAverageStrategy dropLowestStrategy = new DropLowestAverageStrategy();

            Console.WriteLine("--- Testing Simple Average ---");
            service.PrintStudentReport(s1, simpleStrategy);

            Console.WriteLine("\n--- Testing Drop-Lowest Average ---");
            service.PrintStudentReport(s1, dropLowestStrategy);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}