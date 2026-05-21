using CW1After.Models;

namespace CW1After.Services;

public class AverageCalculator
{
    public double Average(Student student)
    {
        if (student.Grades.Count == 0)
            return 0.0;

        int sum = 0;
        foreach (var g in student.Grades)
            sum += g;

        return sum / (double)student.Grades.Count;
    }
}