using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class ReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _averageCalculator;

    public ReportService(
        IStudentRepository repository,
        AverageCalculator averageCalculator)
    {
        _repository = repository;
        _averageCalculator = averageCalculator;
    }

    // ==========================
    // TASK 7 — TOP BY AVERAGE
    // ==========================

    public List<Student> GetTopByAverage(int n)
    {
        return _repository
            .GetAllStudents()
            .OrderByDescending(
                s => _averageCalculator.Calculate(s.Grades))
            .Take(n)
            .ToList();
    }

    public List<Student> GetTopByAverageWithoutLinq(int n)
    {
        var students =
            new List<Student>(
                _repository.GetAllStudents());

        students.Sort((a, b) =>
            _averageCalculator
                .Calculate(b.Grades)
                .CompareTo(
                    _averageCalculator
                        .Calculate(a.Grades)));

        var result = new List<Student>();

        for (int i = 0;
             i < n && i < students.Count;
             i++)
        {
            result.Add(students[i]);
        }

        return result;
    }

    // ==========================
    // TASK 8 — GROUP SORT
    // ==========================

    public List<Student>
        GetStudentsInGroupSortedByName(
            string code)
    {
        return _repository
            .GetAllStudents()
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.Name)
            .ToList();
    }

    public List<Student>
        GetStudentsInGroupSortedByNameWithoutLinq(
            string code)
    {
        var result =
            new List<Student>();

        foreach (var student
                 in _repository.GetAllStudents())
        {
            if (student.GroupCode == code)
            {
                result.Add(student);
            }
        }

        result.Sort((a, b)
            => a.Name.CompareTo(b.Name));

        return result;
    }

    // ==========================
    // TASK 9 — STATISTICS
    // ==========================

    public string GetStatistics()
    {
        var students =
            _repository.GetAllStudents();

        int totalStudents =
            students.Count;

        int totalGrades =
            students.Sum(
                s => s.Grades.Count);

        double meanOfMeans =
            students.Average(
                s => _averageCalculator
                    .Calculate(s.Grades));

        int maxGrade =
            students
                .SelectMany(s => s.Grades)
                .DefaultIfEmpty(0)
                .Max();

        bool hasFailing =
            students.Any(
                s => s.Grades.Any(g => g < 5));

        bool allHaveEmail =
            students.All(
                s => !string.IsNullOrWhiteSpace(
                    s.Email));

        return
            $"Total students: {totalStudents}\n" +
            $"Total grades: {totalGrades}\n" +
            $"Mean of averages: {meanOfMeans:0.00}\n" +
            $"Max grade: {maxGrade}\n" +
            $"Any failing (<5)? {hasFailing}\n" +
            $"All have email? {allHaveEmail}";
    }

    public string GetStatisticsWithoutLinq()
    {
        var students =
            _repository.GetAllStudents();

        int totalStudents = 0;
        int totalGrades = 0;
        double totalAverage = 0;
        int maxGrade = 0;
        bool hasFailing = false;
        bool allHaveEmail = true;

        foreach (var student in students)
        {
            totalStudents++;

            if (string.IsNullOrWhiteSpace(
                student.Email))
            {
                allHaveEmail = false;
            }

            totalAverage +=
                _averageCalculator
                    .Calculate(student.Grades);

            foreach (var grade
                     in student.Grades)
            {
                totalGrades++;

                if (grade > maxGrade)
                {
                    maxGrade = grade;
                }

                if (grade < 5)
                {
                    hasFailing = true;
                }
            }
        }

        double meanOfMeans =
            totalStudents == 0
                ? 0
                : totalAverage /
                  totalStudents;

        return
            $"Total students: {totalStudents}\n" +
            $"Total grades: {totalGrades}\n" +
            $"Mean of averages: {meanOfMeans:0.00}\n" +
            $"Max grade: {maxGrade}\n" +
            $"Any failing (<5)? {hasFailing}\n" +
            $"All have email? {allHaveEmail}";
    }
}