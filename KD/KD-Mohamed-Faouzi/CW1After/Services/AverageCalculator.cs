namespace CW1After.Services;

public class AverageCalculator
{
    public double Calculate(List<int> grades)
    {
        if (grades.Count == 0)
            return 0;

        return grades.Sum() / (double)grades.Count;
    }
}