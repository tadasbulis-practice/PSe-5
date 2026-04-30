
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        if (!students.Any()) return 0;
        return students.Average(s => s.Grade);
    }
}
