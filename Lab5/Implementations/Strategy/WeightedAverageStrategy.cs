using Lab5.Interfaces;
using Lab5.Models;

public class WeightedAverageStrategy : IAverageStrategy
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

        return total / totalWeight;
    }
}
