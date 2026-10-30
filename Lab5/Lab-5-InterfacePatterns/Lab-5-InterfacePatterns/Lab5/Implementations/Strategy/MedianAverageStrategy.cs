using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        var sorted = student.Grades.OrderBy(g => g).ToList();
        int count = sorted.Count;

        if (count % 2 == 1)
            return sorted[count / 2];

        return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
    }
}