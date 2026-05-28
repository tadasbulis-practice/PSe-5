namespace Lab_5.Services;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades == null || grades.Count == 0)
            return 0;

        return grades.Average();
    }
}