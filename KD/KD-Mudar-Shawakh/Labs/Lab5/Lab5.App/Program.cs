using System;
using Lab5.App.Models;
using Lab5.App.Services;
using Lab5.App.Strategies;

namespace Lab5.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Average Calculation Strategy (Variant 2):");
            Console.WriteLine("1. Simple Average");
            Console.WriteLine("2. Weighted Average");
            Console.WriteLine("3. Median Average");
            Console.Write("Choice: ");
            
            string? choice = Console.ReadLine();
            IAverageStrategy selectedStrategy;

            // Runtime behavior switching
            switch (choice)
            {
                case "1":
                    selectedStrategy = new SimpleAverageStrategy();
                    break;
                case "2":
                    selectedStrategy = new WeightedAverageStrategy();
                    break;
                case "3":
                    selectedStrategy = new MedianAverageStrategy();
                    break;
                default:
                    Console.WriteLine("Invalid. Defaulting to Simple Average.");
                    selectedStrategy = new SimpleAverageStrategy();
                    break;
            }

            // We use FakeStudentFinder to satisfy the existing Lab 4 dependency
            IStudentFinder finder = new FakeStudentFinder();
            
            // Clean constructor injection
            IStudentService service = new StudentService(finder, selectedStrategy);

            var s1 = new Student("Mudar");
            service.AddGrade(s1, 60);
            service.AddGrade(s1, 100);
            service.AddGrade(s1, 80);

            Console.WriteLine("\n--- Execution ---");
            // The service calculates based on the strategy injected at runtime
            service.PrintStudentReport(s1);
        }
    }
}