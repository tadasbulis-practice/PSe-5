using CW1Friend.Models;

namespace CW1Friend.Services;

public class AverageCalculator
{
    public double GetAverage(Student student)
    {
        if (student.Grades.Count == 0)
            return 0.0;

        int total = 0;
        for (int i = 0; i < student.Grades.Count; i++)
            total += student.Grades[i];

        return (double)total / student.Grades.Count;
    }
}
