using Lab7.Interfaces;
using Lab7.Models;
using Lab7.Services;

namespace Lab7.Implementations.Menu;

// Task 6 — Full Flow Scenario
// Demonstrates: Add → Retrieve → Validate → Calculate → Print
public class ConsoleMenuService : IMenuService
{
    private readonly StudentService _service;

    public ConsoleMenuService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("=======================================================");
        Console.WriteLine("  LAB-7 — Containers in Object-Oriented Architecture");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        // ── Step 1: Add multiple students ──────────────────────
        Console.WriteLine("STEP 1 — Adding students...");
        Console.WriteLine();

        var alice = new Student(1, "Alice", "alice@uni.com");
        alice.AddGrade(9);
        alice.AddGrade(8);
        alice.AddGrade(10);

        var bob = new Student(2, "Bob", "bob@uni.com");
        bob.AddGrade(7);
        bob.AddGrade(6);
        bob.AddGrade(8);

        var charlie = new Student(3, "Charlie", "charlie@uni.com");
        charlie.AddGrade(5);
        charlie.AddGrade(4);
        charlie.AddGrade(6);

        var diana = new Student(4, "Diana", "diana@uni.com");
        diana.AddGrade(10);
        diana.AddGrade(10);
        diana.AddGrade(9);

        // This one has an invalid email — should be rejected by validator
        var invalid = new Student(5, "InvalidUser", "not-an-email");
        invalid.AddGrade(8);

        _service.AddStudent(alice);
        _service.AddStudent(bob);
        _service.AddStudent(charlie);
        _service.AddStudent(diana);
        _service.AddStudent(invalid);   // will be rejected

        Console.WriteLine();

        // ── Step 2: Retrieve and print all students ─────────────
        Console.WriteLine("STEP 2 — All students in repository:");
        Console.WriteLine();
        _service.PrintAllStudents();
        Console.WriteLine();

        // ── Step 3: Validate students ───────────────────────────
        Console.WriteLine("STEP 3 — Valid students only:");
        Console.WriteLine();
        _service.PrintValidStudents();
        Console.WriteLine();

        // ── Step 4: Calculate group average (3 strategies) ──────
        Console.WriteLine("STEP 4 — Group average using different strategies:");
        Console.WriteLine();

        double avg = _service.CalculateGroupAverage();
        Console.WriteLine($"  Group average: {avg:0.00}");
        Console.WriteLine();

        // ── Step 5: Print sorted and filtered ───────────────────
        Console.WriteLine("STEP 5 — Students sorted by average (best first):");
        Console.WriteLine();
        var sorted = _service.GetStudentsSortedByAverage();
        foreach (var s in sorted)
            Console.WriteLine($"  {s.Name,-12} avg: {s.GetAverage():0.00}");

        Console.WriteLine();
        Console.WriteLine("STEP 5b — Students with average >= 7.0:");
        Console.WriteLine();
        var topStudents = _service.FilterByMinAverage(7.0);
        foreach (var s in topStudents)
            Console.WriteLine($"  {s.Name,-12} avg: {s.GetAverage():0.00}");

        Console.WriteLine();

        // ── Extra: FindById and Remove ───────────────────────────
        Console.WriteLine("EXTRA — Find student by ID=2:");
        var found = _service.FindStudentById(2);
        Console.WriteLine(found is null ? "  Not found." : $"  Found: {found.Name} ({found.Email})");

        Console.WriteLine();
        Console.WriteLine("EXTRA — Remove student ID=3 (Charlie):");
        _service.RemoveStudent(3);

        Console.WriteLine();
        Console.WriteLine("After removal — final list:");
        Console.WriteLine();
        _service.PrintAllStudents();

        Console.WriteLine();
        Console.WriteLine("=======================================================");
        Console.WriteLine("  Done.");
        Console.WriteLine("=======================================================");
    }
}
