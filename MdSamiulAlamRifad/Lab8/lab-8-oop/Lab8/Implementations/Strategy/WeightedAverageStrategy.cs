using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Strategy;

// SOLID — Open/Closed Principle (OCP)
// We add a new calculation strategy WITHOUT changing IAverageStrategy
// or any existing strategy class. Just a new file.
public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyList<Student> students)
    {
        var withGrades = students.Where(s => s.Grades.Count > 0).ToList();
        if (withGrades.Count == 0) return 0;

        double weightedSum = withGrades.Sum(s => s.Grades.Average() * s.Grades.Count);
        int totalGrades    = withGrades.Sum(s => s.Grades.Count);

        return totalGrades == 0 ? 0 : weightedSum / totalGrades;
    }
}
