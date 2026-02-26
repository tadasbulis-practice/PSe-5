using lab3.Edgar.model;

namespace Lab3.Edgar.Service;

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
