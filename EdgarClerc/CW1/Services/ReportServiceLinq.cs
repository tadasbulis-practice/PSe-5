using CW1.Interface;
using CW1.Models;

namespace CW1.Services;

public class ReportServiceLinq : IReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageStategy _averageStategy;

    public ReportServiceLinq(IStudentRepository repository, AverageStategy averageStategy)
    {
        _repository = repository;
        _averageStategy = averageStategy;
    }

    public List<(Student student, double avg)> GetTopByAverage(int numberOfStudents = 3)
    {
        return _repository
            .FindAll()
            .Select(s => (student: s, avg: _averageStategy.GetAverage(s)))
            .OrderByDescending(x => x.avg)
            .Take(numberOfStudents)
            .ToList();
    }

    public List<Student> GetStudentsInGroupSortedByName(string gc)
    {
        return _repository.FindAll().Where(s => s.GroupCode == gc).OrderBy(s => s.Name).ToList();
    }

    public (
        int totalStudents,
        int totalGrades,
        double meanOfMeans,
        float maxGrade,
        bool hasFailing,
        bool allHaveEmail
    ) GetStatistics()
    {
        var students = _repository.FindAll();

        int totalStudents = students.Count;
        int totalGrades = students.Sum(s => s.Grades.Count);

        double meanOfMeans =
            students.Count == 0
                ? 0.0
                : students.Average(s => s.Grades.Count == 0 ? 0.0 : s.Grades.Average());

        float maxGrade = students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
        bool hasFailing = students.Any(s => s.Grades.Any(g => g < 5));
        bool allHaveEmail = students.All(s => !string.IsNullOrWhiteSpace(s.Email));

        return (totalStudents, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }
}
