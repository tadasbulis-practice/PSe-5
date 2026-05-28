public class WeightedAverage : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        return grades.Sum() / (double)grades.Count;
    }
}