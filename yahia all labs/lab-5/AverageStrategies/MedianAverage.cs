public class MedianAverage : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        if (grades == null || grades.Count == 0)
            return 0;

        var sorted = grades.OrderBy(x => x).ToList();
        int count = sorted.Count;

        if (count % 2 == 1)
            return sorted[count / 2];

        return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
    }
}