public class SimpleAverage : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        if (grades == null || grades.Count == 0)
            return 0;

        return grades.Average();
    }
}