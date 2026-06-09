using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Printer;

// Task 2 — Printer for Collections
public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        Console.WriteLine("┌─────────────────────────────────────────────────────┐");
        Console.WriteLine($"│  Students ({students.Count} total)");
        Console.WriteLine("├─────────────────────────────────────────────────────┤");

        if (students.Count == 0)
        {
            Console.WriteLine("│  (no students)");
        }

        foreach (var s in students)
        {
            var grades = s.Grades.Count > 0 ? string.Join(", ", s.Grades) : "no grades";
            var avg = s.Grades.Count > 0 ? $"avg: {s.GetAverage():0.00}" : "avg: -";
            Console.WriteLine($"│  [{s.Id}] {s.Name} | {s.Email} | {grades} | {avg}");
        }

        Console.WriteLine("└─────────────────────────────────────────────────────┘");
    }
}
