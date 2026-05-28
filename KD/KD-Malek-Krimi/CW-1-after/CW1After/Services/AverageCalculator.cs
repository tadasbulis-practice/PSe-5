namespace CW1After.Services;

public class AverageCalculator
{
    public double CalculateAverage(List<int> grades)
    {
        if (grades.Count == 0)
        {
            return 0.0;
        }

        int sum = 0;
        foreach (int grade in grades)
        {
            sum += grade;
        }

        return sum / (double)grades.Count;
    }
}
