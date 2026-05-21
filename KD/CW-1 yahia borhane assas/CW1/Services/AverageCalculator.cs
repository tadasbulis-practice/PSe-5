namespace CW1After.Services;

public class AverageCalculator
{
    public double Calculate(List<int> grades)
    {
        if (grades.Count == 0)
        {
            return 0;
        }

        double sum = 0;

        foreach (int grade in grades)
        {
            sum += grade;
        }

        return sum / grades.Count;
    }
}