public class StudentReportService
{
    private readonly IAverageStrategy _averageStrategy;

    public StudentReportService(IAverageStrategy averageStrategy)
    {
        _averageStrategy = averageStrategy;
    }

    public string GetStudentStatus(Student student)
    {
        double average = _averageStrategy.Calculate(student);

        if (average >= 5.0)
        {
            return $"Student: {student.FirstName} | Average: {average:0.00} | Status: PASSED";
        }

        return $"Student: {student.FirstName} | Average: {average:0.00} | Status: FAILED";
    }
}