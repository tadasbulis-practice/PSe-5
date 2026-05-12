using Lab5.Interfaces;
using Lab5.Models;

public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        var sorted = student.Grades.OrderBy(x => x).ToList();
        int count = sorted.Count;

        if (count % 2 == 1)
            return sorted[count / 2];

        return (sorted[count / 2] + sorted[(count / 2) - 1]) / 2.0;
    }
}
