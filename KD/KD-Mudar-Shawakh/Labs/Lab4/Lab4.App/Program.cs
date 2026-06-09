using System;
using Lab4.App.Models;
using Lab4.App.Services;

namespace Lab4.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Dependency Implementation:");
            Console.WriteLine("1. Fake (Simulates unfinished module)");
            Console.WriteLine("2. Stub - Success Branch");
            Console.WriteLine("3. Stub - Fail Branch");
            Console.Write("Choice: ");
            
            string choice = Console.ReadLine();
            IStudentFinder selectedFinder;

            switch (choice)
            {
                case "1":
                    selectedFinder = new FakeStudentFinder();
                    break;
                case "2":
                    selectedFinder = new StubStudentFinder(new Student("Injected Stub Student"));
                    break;
                case "3":
                    selectedFinder = new StubStudentFinder(null);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Fake.");
                    selectedFinder = new FakeStudentFinder();
                    break;
            }

            IStudentService service = new StudentService(selectedFinder);
            Group dummyGroup = new Group("Test Group");

            Console.WriteLine("\n--- Executing Business Logic ---");
            service.SearchAndDisplayStudent(dummyGroup, "Any Query");
        }
    }
}