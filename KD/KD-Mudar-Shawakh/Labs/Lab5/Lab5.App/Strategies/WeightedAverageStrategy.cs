using Lab5.App.Models;

namespace Lab5.App.Strategies
{
    public class WeightedAverageStrategy : IAverageStrategy
    {
        public double Calculate(Student s)
        {
            if (s.Grades.Count == 0) return 0;

            double totalWeight = 0;
            double weightedSum = 0;

            // Simulating weights: later grades have a higher weight (index + 1)
            for (int i = 0; i < s.Grades.Count; i++)
            {
                double weight = i + 1; 
                weightedSum += s.Grades[i] * weight;
                totalWeight += weight;
            }

            return weightedSum / totalWeight;
        }
    }
}