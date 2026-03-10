
using Week3.LiveCoding.Advanced.Models;

namespace Week3.LiveCoding.Advanced.Strategies;

public class AverageStrategy : IGradeStrategy
{
    public double Calculate(Student student)
    {
        return student.Grades.Average();
    }
}
public class MedianAverageStrategy : IAverageStrategy
{
    public double Calculate(List<int> grades)
    {
        var sorted = grades.OrderBy(x => x).ToList();
        int mid = sorted.Count / 2;
        return sorted[mid];
    }
}

