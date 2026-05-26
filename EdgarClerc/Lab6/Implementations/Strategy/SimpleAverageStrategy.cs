using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Strategy;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        return student.Grades.Average();
    }
}
