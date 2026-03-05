using System.Linq;

public class WeightedAverageStrategy : IAverageStrategy
{
    public double Calculate(Student s)
        => s.Grades.Average() * 1.1;
}