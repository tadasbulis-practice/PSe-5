using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class ReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _averageCalculator;

    public ReportService(IStudentRepository repository, AverageCalculator averageCalculator)
    {
        _repository = repository;
        _averageCalculator = averageCalculator;
    }

    public List<StudentAverageReport> GetTopByAverage(int n)
    {
        return _repository.GetStudents()
            .Select(s => new StudentAverageReport
            {
                Student = s,
                Average = _averageCalculator.CalculateAverage(s.Grades)
            })
            .OrderByDescending(x => x.Average)
            .Take(n)
            .ToList();
    }

    public List<StudentAverageReport> GetTopByAverageWithoutLinq(int n)
    {
        var result = new List<StudentAverageReport>();
        foreach (var student in _repository.GetStudents())
        {
            result.Add(new StudentAverageReport
            {
                Student = student,
                Average = _averageCalculator.CalculateAverage(student.Grades)
            });
        }

        result.Sort((a, b) => b.Average.CompareTo(a.Average));

        var top = new List<StudentAverageReport>();
        int limit = n;
        if (result.Count < limit)
        {
            limit = result.Count;
        }

        for (int i = 0; i < limit; i++)
        {
            top.Add(result[i]);
        }

        return top;
    }

    public List<Student> GetStudentsInGroupSortedByName(string code)
    {
        return _repository.GetStudents()
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.Name)
            .ToList();
    }

    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string code)
    {
        var result = new List<Student>();
        foreach (var student in _repository.GetStudents())
        {
            if (student.GroupCode == code)
            {
                result.Add(student);
            }
        }

        result.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
        return result;
    }

    public ReportStatistics GetStatistics()
    {
        var students = _repository.GetStudents();
        return new ReportStatistics
        {
            TotalStudents = students.Count,
            TotalGrades = students.Sum(s => s.Grades.Count),
            MeanOfAverages = students.Count == 0 ? 0.0 : students.Average(s => _averageCalculator.CalculateAverage(s.Grades)),
            MaxGrade = students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max(),
            HasFailing = students.Any(s => s.Grades.Any(g => g < 5)),
            AllHaveEmail = students.All(s => !string.IsNullOrWhiteSpace(s.Email))
        };
    }

    public ReportStatistics GetStatisticsWithoutLinq()
    {
        var students = _repository.GetStudents();
        var statistics = new ReportStatistics
        {
            TotalStudents = students.Count,
            TotalGrades = 0,
            MeanOfAverages = 0.0,
            MaxGrade = 0,
            HasFailing = false,
            AllHaveEmail = true
        };

        double sumOfAverages = 0.0;

        foreach (var student in students)
        {
            statistics.TotalGrades += student.Grades.Count;
            sumOfAverages += _averageCalculator.CalculateAverage(student.Grades);

            if (string.IsNullOrWhiteSpace(student.Email))
            {
                statistics.AllHaveEmail = false;
            }

            foreach (int grade in student.Grades)
            {
                if (grade > statistics.MaxGrade)
                {
                    statistics.MaxGrade = grade;
                }

                if (grade < 5)
                {
                    statistics.HasFailing = true;
                }
            }
        }

        if (students.Count > 0)
        {
            statistics.MeanOfAverages = sumOfAverages / students.Count;
        }

        return statistics;
    }
}
