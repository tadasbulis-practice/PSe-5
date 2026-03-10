using Lab3.StrategyDemo.Interfaces;

namespace Lab3.StrategyDemo.Strategies;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(IReadOnlyCollection<int> grades)
    {
        if (grades.Count == 0) return 0;
        return grades.Average();
    }
}