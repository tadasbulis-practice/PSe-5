using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Implementations;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(List<Student> students)
    {
        if (students.Count == 0) return 0;

        double total = 0;
        int count = 0;

        foreach (var s in students)
        {
            foreach (var g in s.Grades)
            {
                total += g;
                count++;
            }
        }

        return count == 0 ? 0 : total / count;
    }
}