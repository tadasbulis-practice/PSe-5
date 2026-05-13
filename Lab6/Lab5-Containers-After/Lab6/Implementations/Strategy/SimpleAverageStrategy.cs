using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Strategy;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyList<Student> students)
    {
        var allGrades = students.SelectMany(student => student.Grades).ToList();

        if (allGrades.Count == 0)
        {
            return 0;
        }

        return allGrades.Average();
    }
}
