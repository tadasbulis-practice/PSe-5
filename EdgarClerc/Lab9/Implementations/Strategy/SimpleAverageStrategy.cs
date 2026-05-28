using Lab9.Interfaces;
using Lab9.Models;

namespace Lab9.Implementations.Strategy;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyList<Student> students)
    {
        if (students.Count == 0)
            return 0;
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        return allGrades.Count == 0 ? 0 : allGrades.Average();
    }
}
