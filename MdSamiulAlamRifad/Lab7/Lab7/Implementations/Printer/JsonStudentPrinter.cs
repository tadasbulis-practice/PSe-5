using System.Text.Json;
using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Printer;

// Task 2 — JsonStudentPrinter
public class JsonStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public JsonStudentPrinter(string filePath = "students.json")
    {
        _filePath = filePath;
    }

    public void PrintStudents(List<Student> students)
    {
        var data = students.Select(s => new
        {
            s.Id,
            s.Name,
            s.Email,
            Grades = s.Grades.ToList(),
            Average = s.GetAverage()
        });

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
        Console.WriteLine($"[JsonStudentPrinter] Saved {students.Count} students to '{_filePath}'");
    }
}
