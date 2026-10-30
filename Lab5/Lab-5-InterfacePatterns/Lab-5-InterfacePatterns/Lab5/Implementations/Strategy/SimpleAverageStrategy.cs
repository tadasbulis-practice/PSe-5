
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        return student.Grades.Average();
    }
}
