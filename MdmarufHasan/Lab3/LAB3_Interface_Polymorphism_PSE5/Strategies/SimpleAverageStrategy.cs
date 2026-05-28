using System.Linq;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(Student s)
        => s.Grades.Average();
}