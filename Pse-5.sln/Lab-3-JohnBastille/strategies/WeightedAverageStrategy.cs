using Lab3.StrategyDemo.Interfaces;

namespace Lab3.StrategyDemo.Strategies;

public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyCollection<int> grades)
    {
        if (grades.Count == 0) return 0;

        // Simple “heavier weight for later grades” example
        var list = grades.ToList();
        double sum = 0;
        double weightSum = 0;

        for (int i = 0; i < list.Count; i++)
        {
            double weight = i + 1; // later grades weigh more
            sum += list[i] * weight;
            weightSum += weight;
        }

        return sum / weightSum;
    }
}