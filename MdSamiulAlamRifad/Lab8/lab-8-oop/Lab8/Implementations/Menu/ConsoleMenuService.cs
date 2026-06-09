using Lab8.Interfaces;
using Lab8.Services;

namespace Lab8.Implementations.Menu;

public class ConsoleMenuService : IMenuService
{
    private readonly StudentService   _service;
    private readonly ISolidDemoService _solidDemo;

    public ConsoleMenuService(StudentService service, ISolidDemoService solidDemo)
    {
        _service   = service;
        _solidDemo = solidDemo;
    }

    public void Run()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════╗");
        Console.WriteLine("║  LAB-8 — Repository + API + SOLID Principles ║");
        Console.WriteLine("╚══════════════════════════════════════════════╝");

        while (true)
        {
            Console.WriteLine("\n  ── Students ────────────────────────────────");
            Console.WriteLine("  1. Show all students");
            Console.WriteLine("  2. Find student by ID");
            Console.WriteLine("  3. Calculate group average");
            Console.WriteLine("  ── Groups & Faculty ────────────────────────");
            Console.WriteLine("  4. List all groups");
            Console.WriteLine("  5. Show group details");
            Console.WriteLine("  6. Show full faculty structure");
            Console.WriteLine("  ── SOLID Demo ──────────────────────────────");
            Console.WriteLine("  7. Run SOLID principles demo");
            Console.WriteLine("  ────────────────────────────────────────────");
            Console.WriteLine("  0. Exit");
            Console.Write("\n  Your choice: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    _service.PrintAllStudents();
                    break;

                case "2":
                    Console.Write("  Student ID: ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        var s = _service.FindStudentById(id);
                        Console.WriteLine(s is null
                            ? "  Not found."
                            : $"\n  {s.Id,-5} {s.FullName,-25} {s.Email,-30} {s.StudyProgram} {s.EnrollmentYear}");
                    }
                    break;

                case "3":
                    Console.WriteLine($"\n  Average grade: {_service.CalculateGroupAverage():0.00}");
                    break;

                case "4":
                    var groups = _service.GetAllGroups();
                    Console.WriteLine($"\n  Groups ({groups.Count} total):");
                    foreach (var g in groups)
                        Console.WriteLine($"  [{g.Code,-8}]  {g.StudyProgram,-25} {g.EnrollmentYear}  — {g.Students.Count} students");
                    break;

                case "5":
                    Console.Write("  Group code (e.g. CS-23): ");
                    _service.PrintGroup(Console.ReadLine()?.Trim() ?? "");
                    break;

                case "6":
                    _service.PrintFaculty();
                    break;

                case "7":
                    _solidDemo.RunSolidDemo();
                    break;

                case "0":
                    Console.WriteLine("\n  Goodbye!\n");
                    return;

                default:
                    Console.WriteLine("  Unknown option.");
                    break;
            }
        }
    }
}
