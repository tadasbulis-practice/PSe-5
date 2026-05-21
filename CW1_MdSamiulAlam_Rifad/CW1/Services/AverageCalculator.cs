using CW1After.Models;

namespace CW1After.Services;

public class AverageCalculator
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0) return 0.0;

        double sum = 0;
        foreach (var g in student.Grades)
            sum += g;

        return sum / student.Grades.Count;
    }
}
