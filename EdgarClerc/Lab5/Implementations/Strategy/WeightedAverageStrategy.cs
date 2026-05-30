using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Strategy;

public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        int[] weightList = { 1, 2, 4 };

        if (student.Grades.Count == 0)
            return 0;

        double total = 0;
        int totalWeight = 0;

        for (int i = 0; i < student.Grades.Count; i++)
        {
            int weight = weightList[i % weightList.Length];
            total += student.Grades[i] * weight;
            totalWeight += weight;
        }

        return total / totalWeight;
    }
}
