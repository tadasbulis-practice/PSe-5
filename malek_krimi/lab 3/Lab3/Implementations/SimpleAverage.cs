public class SimpleAverage : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        return grades.Average();
    }
}