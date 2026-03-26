using lab4.model;

namespace Lab4.Service;

public class AverageStrategy : IAverageStrategy
{
    public double Average(StudentProfile student)
    {
        return student.Grades.Average();
    }
}
