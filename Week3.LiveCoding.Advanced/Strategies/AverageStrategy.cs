
using Week3.LiveCoding.Advanced.Models;

namespace Week3.LiveCoding.Advanced.Strategies;

public class AverageStrategy : IGradeStrategy
{
    public double Calculate(Student student)
    {
        return student.Grades.Average();
    }
}
