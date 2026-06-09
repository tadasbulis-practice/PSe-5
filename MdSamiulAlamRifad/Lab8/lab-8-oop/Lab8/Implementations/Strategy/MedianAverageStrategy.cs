using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Strategy;

// SOLID — Open/Closed Principle (OCP)
// Third strategy variant. Same interface, zero changes elsewhere.
public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyList<Student> students)
    {
        var allGrades = students
            .SelectMany(s => s.Grades)
            .OrderBy(g => g)
            .ToList();

        if (allGrades.Count == 0) return 0;

        int mid = allGrades.Count / 2;
        return allGrades.Count % 2 == 0
            ? (allGrades[mid - 1] + allGrades[mid]) / 2.0
            : allGrades[mid];
    }
}
