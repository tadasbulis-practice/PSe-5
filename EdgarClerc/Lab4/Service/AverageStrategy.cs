using lab3.model;

namespace Lab3.Service;

public class AverageStrategy : IAverageStrategy
{
    public double Average(StudentProfile student)
    {
        return student.Grades.Average();
    }
}
