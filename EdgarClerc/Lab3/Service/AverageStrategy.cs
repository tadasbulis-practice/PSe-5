using lab3.model;

namespace Lab3.Service;

public class AverageStrategy : IAverageStrategy
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    public double Average(StudentProfile student)
    {
        return student.Grades.Average();
    }
}
