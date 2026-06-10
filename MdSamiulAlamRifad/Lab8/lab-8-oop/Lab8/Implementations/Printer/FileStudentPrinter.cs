using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Printer;

// SOLID — Single Responsibility Principle (SRP) + Open/Closed (OCP)
// ConsoleStudentPrinter prints to screen — that's its only job.
// FileStudentPrinter prints to disk — that's its only job.
// Adding file output did NOT require changing ConsoleStudentPrinter.
public class FileStudentPrinter : IStudentPrinter
{
    private readonly string _filePath;

    public FileStudentPrinter(string filePath = "students_export.txt")
    {
        _filePath = filePath;
    }

    public void PrintStudents(IReadOnlyList<Student> students)
    {
        var lines = new List<string>
        {
            $"Students export — {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
            new string('-', 60)
        };

        foreach (var s in students)
            lines.Add($"[{s.Id}] {s.FullName} | {s.Email} | {s.StudyProgram} | {s.EnrollmentYear}");

        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"  [FileStudentPrinter] {students.Count} students saved to '{_filePath}'");
    }

    public void PrintGroup(Group group)
    {
        var lines = new List<string>
        {
            $"Group: [{group.Code}] {group.StudyProgram} ({group.EnrollmentYear})",
            new string('-', 60)
        };

        foreach (var s in group.Students)
            lines.Add($"[{s.Id}] {s.FullName} | {s.Email}");

        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"  [FileStudentPrinter] Group {group.Code} saved to '{_filePath}'");
    }

    public void PrintFaculty(Faculty faculty)
    {
        var lines = new List<string>
        {
            $"Faculty: {faculty.Name}",
            $"Groups: {faculty.Groups.Count} | Total students: {faculty.TotalStudents}",
            new string('-', 60)
        };

        foreach (var g in faculty.Groups)
            lines.Add($"  [{g.Code}] {g.StudyProgram} {g.EnrollmentYear} — {g.Students.Count} students");

        File.WriteAllLines(_filePath, lines);
        Console.WriteLine($"  [FileStudentPrinter] Faculty saved to '{_filePath}'");
    }
}
