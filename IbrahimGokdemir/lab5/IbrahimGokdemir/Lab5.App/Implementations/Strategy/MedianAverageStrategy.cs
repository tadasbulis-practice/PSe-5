using System.Linq;

public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        var sorted = student.Grades.OrderBy(x => x).ToList();
        int count = sorted.Count;

        if (count == 0)
            return 0;

        if (count % 2 == 1)
            return sorted[count / 2];

        return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
    }
}