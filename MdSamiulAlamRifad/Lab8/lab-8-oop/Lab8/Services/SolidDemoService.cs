using Lab8.Implementations.Printer;
using Lab8.Implementations.Strategy;
using Lab8.Implementations.Validator;
using Lab8.Interfaces;
using Lab8.Models;
using Lab8.Services;

namespace Lab8.Services;

// SOLID demonstration service.
// Shows each principle with a concrete before/after explanation.
public class SolidDemoService : ISolidDemoService
{
    private readonly StudentService _service;

    public SolidDemoService(StudentService service)
    {
        _service = service;
    }

    public void RunSolidDemo()
    {
        Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║          SOLID PRINCIPLES — Live Demo                ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");

        DemoSRP();
        DemoOCP();
        DemoLSP();
        DemoISP();
        DemoDIP();

        Console.WriteLine("\n══════════════════════════════════════════════════════");
        Console.WriteLine("  SOLID Demo complete.");
        Console.WriteLine("══════════════════════════════════════════════════════\n");
    }

    // ── S — Single Responsibility Principle ────────────────────────────
    private void DemoSRP()
    {
        Console.WriteLine("\n── S: Single Responsibility Principle ─────────────────");
        Console.WriteLine("  Each class has ONE reason to change.");
        Console.WriteLine("  ConsoleStudentPrinter  → only knows how to print to console");
        Console.WriteLine("  FileStudentPrinter     → only knows how to print to a file");
        Console.WriteLine("  MemoryStudentRepository → only knows how to store/retrieve students");
        Console.WriteLine("  StudentService          → only orchestrates business logic");
        Console.WriteLine();

        var students = _service.GetAllStudents();

        Console.WriteLine("  [SRP] Printing to console (ConsoleStudentPrinter):");
        IStudentPrinter consolePrinter = new ConsoleStudentPrinter();
        consolePrinter.PrintStudents(students);

        Console.WriteLine("\n  [SRP] Printing to file (FileStudentPrinter):");
        IStudentPrinter filePrinter = new FileStudentPrinter("srp_demo.txt");
        filePrinter.PrintStudents(students);
    }

    // ── O — Open/Closed Principle ──────────────────────────────────────
    private void DemoOCP()
    {
        Console.WriteLine("\n── O: Open/Closed Principle ────────────────────────────");
        Console.WriteLine("  Classes are open for EXTENSION, closed for MODIFICATION.");
        Console.WriteLine("  We added 3 strategies without changing StudentService at all.");
        Console.WriteLine();

        var students = _service.GetAllStudents().ToList();

        // Give everyone some grades for demo
        var demo = new List<Student>();
        var grades = new[] { new[]{8,9,7}, new[]{6,7,8}, new[]{5,6,7}, new[]{9,10,9} };
        int i = 0;
        foreach (var s in students.Take(4))
        {
            var copy = new Student(s.Id, s.FirstName, s.LastName, s.Email, s.StudyProgram, s.EnrollmentYear);
            foreach (var g in grades[i % grades.Length]) copy.AddGrade(g);
            demo.Add(copy);
            i++;
        }

        IAverageStrategy simple   = new SimpleAverageStrategy();
        IAverageStrategy weighted = new WeightedAverageStrategy();
        IAverageStrategy median   = new MedianAverageStrategy();

        Console.WriteLine($"  Simple   average: {simple.Calculate(demo):0.00}");
        Console.WriteLine($"  Weighted average: {weighted.Calculate(demo):0.00}");
        Console.WriteLine($"  Median   average: {median.Calculate(demo):0.00}");
        Console.WriteLine("  → Same interface, 3 behaviours, StudentService unchanged.");
    }

    // ── L — Liskov Substitution Principle ─────────────────────────────
    private void DemoLSP()
    {
        Console.WriteLine("\n── L: Liskov Substitution Principle ────────────────────");
        Console.WriteLine("  Any IStudentValidator implementation can replace another.");
        Console.WriteLine("  The system works correctly with both.");
        Console.WriteLine();

        var students = _service.GetAllStudents().ToList();

        IStudentValidator lenient = new Implementations.Adapter.StudentValidatorAdapter();
        IStudentValidator strict  = new StrictStudentValidator();

        var validLenient = lenient.ValidateAll(students);
        var validStrict  = strict.ValidateAll(students);

        Console.WriteLine($"  Lenient validator: {validLenient.Count}/{students.Count} students valid");
        Console.WriteLine($"  Strict  validator: {validStrict.Count}/{students.Count} students valid");
        Console.WriteLine("  → Swapped validator, service behaviour unchanged.");
    }

    // ── I — Interface Segregation Principle ───────────────────────────
    private void DemoISP()
    {
        Console.WriteLine("\n── I: Interface Segregation Principle ──────────────────");
        Console.WriteLine("  Clients depend only on what they need.");
        Console.WriteLine();
        Console.WriteLine("  IMenuService       → void Run()                       (1 method)");
        Console.WriteLine("  IStudentPrinter    → Print* methods                   (3 methods)");
        Console.WriteLine("  IStudentValidator  → Validate + ValidateAll            (2 methods)");
        Console.WriteLine("  IAverageStrategy   → double Calculate(...)             (1 method)");
        Console.WriteLine("  IStudentRepository → GetAll/GetById/Add/Remove/Groups  (7 methods)");
        Console.WriteLine("  ISolidDemoService  → void RunSolidDemo()               (1 method)");
        Console.WriteLine();
        Console.WriteLine("  → No class is forced to implement methods it doesn't need.");
    }

    // ── D — Dependency Inversion Principle ────────────────────────────
    private void DemoDIP()
    {
        Console.WriteLine("\n── D: Dependency Inversion Principle ───────────────────");
        Console.WriteLine("  High-level modules depend on abstractions, not concrete classes.");
        Console.WriteLine();
        Console.WriteLine("  StudentService depends on:");
        Console.WriteLine("    IStudentRepository  (not MemoryStudentRepository or ApiStudentRepository)");
        Console.WriteLine("    IStudentPrinter     (not ConsoleStudentPrinter or FileStudentPrinter)");
        Console.WriteLine("    IAverageStrategy    (not SimpleAverageStrategy)");
        Console.WriteLine("    IStudentValidator   (not StudentValidatorAdapter)");
        Console.WriteLine();
        Console.WriteLine("  Program.cs is the ONLY place that knows which concrete class is used.");
        Console.WriteLine("  Switching from memory → API required changing ONE line in Program.cs.");
        Console.WriteLine("  StudentService.cs was NOT touched.");
    }
}
