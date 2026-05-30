using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Strategy;

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
