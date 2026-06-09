using System;
using Lab7.App.Interfaces;
using Lab7.App.Services;

namespace Lab7.App.Implementations.Menu
{
    public class ConsoleMenuService : IMenuService
    {
        private readonly StudentService _service;

        public ConsoleMenuService(StudentService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("   LAB-7  —  SOLID + Repository Demo");
            Console.WriteLine("==========================================");

            while (true)
            {
                ShowMenu();
                Console.Write("\n  Choice: ");
                var input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1": _service.PrintAllStudents(); break;
                    case "2": FindById(); break;
                    case "3": ShowGroupAverage(); break;
                    case "4": ListAllGroups(); break;
                    case "5": ShowGroupDetails(); break;
                    case "6": _service.PrintFaculty(); break;
                    case "0":
                        Console.WriteLine("\n  Goodbye!\n");
                        return;
                    default:
                        Console.WriteLine("  Unknown option.");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n  -- Students --");
            Console.WriteLine("  1. Show all students");
            Console.WriteLine("  2. Find student by ID");
            Console.WriteLine("  3. Calculate group average");
            Console.WriteLine("  -- Groups & Faculty --");
            Console.WriteLine("  4. List all groups");
            Console.WriteLine("  5. Show group details");
            Console.WriteLine("  6. Show full faculty structure");
            Console.WriteLine("  -----------------------");
            Console.WriteLine("  0. Exit");
        }

        private void FindById()
        {
            Console.Write("  Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("  Invalid ID.");
                return;
            }

            var s = _service.FindStudentById(id);
            if (s == null)
            {
                Console.WriteLine("  Not found.");
                return;
            }

            Console.WriteLine($"\n  [{s.Id}] {s.FullName} | {s.Email} | {s.StudyProgram} ({s.EnrollmentYear})");
        }

        private void ShowGroupAverage()
        {
            double avg = _service.CalculateGroupAverage();
            Console.WriteLine($"\n  Faculty-wide average grade (valid students only): {avg:F2}");
        }

        private void ListAllGroups()
        {
            var groups = _service.GetAllGroups();
            Console.WriteLine($"\n  Groups ({groups.Count} total):");
            foreach (var g in groups)
            {
                Console.WriteLine($"    [{g.Code,-8}]  {g.StudyProgram,-25} {g.EnrollmentYear}  — {g.Students.Count} students");
            }
        }

        private void ShowGroupDetails()
        {
            Console.Write("  Group code (e.g. CS-23): ");
            var code = Console.ReadLine()?.Trim() ?? "";
            _service.PrintGroup(code);
        }
    }
}