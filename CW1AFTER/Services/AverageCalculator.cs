using CW1After.Models;

namespace CW1After.Services;

public class AverageCalculator
{
    public double Calculate(Student student)
    {
        if (student.Grades.Count == 0) // to avoid no data case
            return 0.0;

        int sum = 0;
        foreach (var grade in student.Grades)
            sum += grade;

        return sum / (double)student.Grades.Count;
    }
}