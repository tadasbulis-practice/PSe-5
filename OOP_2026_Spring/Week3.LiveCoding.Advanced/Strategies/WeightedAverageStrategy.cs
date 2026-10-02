
using Week3.LiveCoding.Advanced.Models;

namespace Week3.LiveCoding.Advanced.Strategies;

public class WeightedAverageStrategy : IGradeStrategy
{
    public double Calculate(Student student)
    {
        double total = 0;
        int weight = 1;
        int totalWeight = 0;

        foreach (var g in student.Grades)
        {
            total += g * weight;
            totalWeight += weight;
            weight++;
        }

        // INTENTIONAL BUG:
        // Should divide by totalWeight
        return total / student.Grades.Count;
    }
}
