using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Strategy;

// Task 3 — MedianAverageStrategy
// Takes all grades from all students, sorts them, and returns the middle value.
// Less affected by extreme outliers than a simple average.
public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
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
