using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Printer;

// Task 2 — FileStudentPrinter
public class FileStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public FileStudentPrinter(string filePath = "students.txt")
    {
        _filePath = filePath;
    }

    public void PrintStudents(List<Student> students)
    {
        var lines = new List<string>();
        lines.Add($"Students export — {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        lines.Add(new string('-', 50));

        foreach (var s in students)
        {
            var grades = s.Grades.Count > 0 ? string.Join(", ", s.Grades) : "no grades";
            lines.Add($"[{s.Id}] {s.Name} | {s.Email} | Grades: {grades} | Avg: {s.GetAverage():0.00}");
        }

        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"[FileStudentPrinter] Saved {students.Count} students to '{_filePath}'");
    }
}
