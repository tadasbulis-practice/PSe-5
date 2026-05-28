using JohnBastille.Lab_4.Interfaces;
using JohnBastille.Lab_4.Models;

namespace Lab_4.Services;

/// <summary>
/// Stub implementation of IStudentPrinter for testing.
/// Captures printed output instead of writing to console.
/// </summary>
public class StubStudentPrinter : IStudentPrinter
{
    public List<string> PrintedOutput { get; } = new();

    public void Print(Student student)
    {
        string output = $"Student: {student.Name}, Age: {student.Age}, Average: {student.GetAverage():F2}";
        PrintedOutput.Add(output);
        // Don't print to console for testing
    }
}