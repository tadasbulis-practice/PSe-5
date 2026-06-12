using System.Linq;

public class SimpleAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        return student.Grades.Average();
    }
}