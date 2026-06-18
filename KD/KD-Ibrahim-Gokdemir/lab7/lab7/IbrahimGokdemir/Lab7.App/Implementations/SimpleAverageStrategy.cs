using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyList<Student> students)
    {
        // Lab-6 used two nested foreach loops to flatten + sum.
        // SelectMany flattens every student's Grades into one sequence;
        // Average does the rest. Same result, no manual loop.
        var allGrades = students.SelectMany(s => s.Grades).ToList();
        return allGrades.Count == 0 ? 0 : allGrades.Average();
    }
}
