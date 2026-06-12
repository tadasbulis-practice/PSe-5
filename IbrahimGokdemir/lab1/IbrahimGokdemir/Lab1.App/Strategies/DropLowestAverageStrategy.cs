using System.Linq;

public class DropLowestAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count <= 1)
            return student.Grades.Average();

        var sorted = student.Grades.OrderBy(x => x).ToList();
        sorted.RemoveAt(0); // drop lowest
        return sorted.Average();
    }
}