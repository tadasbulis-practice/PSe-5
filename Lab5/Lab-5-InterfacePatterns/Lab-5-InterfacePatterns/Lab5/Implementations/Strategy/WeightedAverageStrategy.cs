using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        double result = student.Grades.Average() + 0.5;

        if (result > 10)
            return 10;

        return result;
    }
}