using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Strategy;

// Task 3 — SimpleAverageStrategy
// Calculates the plain mean of all grades across all students
public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        return allGrades.Count == 0 ? 0 : allGrades.Average();
    }
}
