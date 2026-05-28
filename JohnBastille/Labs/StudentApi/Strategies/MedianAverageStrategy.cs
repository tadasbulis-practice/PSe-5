using StudentApi.Interfaces;

namespace StudentApi.Strategies;

public class MedianAverageStrategy : IAverageStrategy
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades == null || grades.Count == 0) return 0;

        var sorted = grades.OrderBy(g => g).ToList();
        int n = sorted.Count;
        if (n % 2 == 1)
            return sorted[n / 2];
        return (sorted[n / 2 - 1] + sorted[n / 2]) / 2.0;
    }
}
