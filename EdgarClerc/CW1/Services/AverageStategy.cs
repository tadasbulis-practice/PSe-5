using CW1.Models;

namespace CW1.Services;

public class AverageStategy
{
    public double GetAverage(Student student)
    {
        return student.Grades.Average();
    }
}
