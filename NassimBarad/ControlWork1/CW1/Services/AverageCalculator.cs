using CW1After.Models;

namespace CW1After.Services;


/// Single source of truth for the average grade formula (DRY principle).
/// Used by StudentService, ReportService, and ConsoleMenu display helpers.

public static class AverageCalculator
{
    public static double Calculate(Student student)
    {
        if (student.Grades.Count == 0) return 0.0;
        int total = 0;
        foreach (var grade in student.Grades)
            total += grade;
        return total / (double)student.Grades.Count;
    }
}
