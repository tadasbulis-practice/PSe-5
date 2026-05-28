using CW1.Models;

namespace CW1.Interface;

public interface IReportService
{
    List<(Student student, double avg)> GetTopByAverage(int numberOfStudents = 3);

    List<Student> GetStudentsInGroupSortedByName(string gc);

    (
        int totalStudents,
        int totalGrades,
        double meanOfMeans,
        int maxGrade,
        bool hasFailing,
        bool allHaveEmail
    ) GetStatistics();
}
