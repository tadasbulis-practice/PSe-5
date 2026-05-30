using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Strategy;

public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0)
            return 0;

        var list = student.Grades.ToList();
        list.Sort();

        return list[list.Count / 2];
    }
}
