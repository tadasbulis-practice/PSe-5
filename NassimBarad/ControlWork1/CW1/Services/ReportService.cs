using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;


// Simple DTO to carry report results without coupling callers to anonymous types.

public record StudentWithAvg(Student Student, double Avg);

public record Statistics(
    int    TotalStudents,
    int    TotalGrades,
    double MeanOfMeans,
    int    MaxGrade,
    bool   HasFailing,
    bool   AllHaveEmail
);


public class ReportService
{
    private readonly IStudentRepository _repository;

    public ReportService(IStudentRepository repository)
    {
        _repository = repository;
    }


    public List<StudentWithAvg> GetTopByAverage(int n) =>
        _repository.GetAllStudents()
            .Select(s => new StudentWithAvg(s, AverageCalculator.Calculate(s)))
            .OrderByDescending(x => x.Avg)
            .Take(n)
            .ToList();

  
       public List<StudentWithAvg> GetTopByAverageWithoutLinq(int n)
    {
        var list = new List<StudentWithAvg>();
        foreach (var s in _repository.GetAllStudents())
            list.Add(new StudentWithAvg(s, AverageCalculator.Calculate(s)));

        // Sort descending by average using a Comparison<T> delegate.
        list.Sort((a, b) => b.Avg.CompareTo(a.Avg));

        var result = new List<StudentWithAvg>();
        for (int i = 0; i < n && i < list.Count; i++)
            result.Add(list[i]);

        return result;
    }


    public List<StudentWithAvg> GetStudentsInGroupSortedByName(string groupCode) =>
        _repository.GetAllStudents()
            .Where(s => s.GroupCode == groupCode)
            .OrderBy(s => s.Name)
            .Select(s => new StudentWithAvg(s, AverageCalculator.Calculate(s)))
            .ToList();

    public List<StudentWithAvg> GetStudentsInGroupSortedByNameWithoutLinq(string groupCode)
    {
        var list = new List<StudentWithAvg>();
        foreach (var s in _repository.GetAllStudents())
        {
            if (s.GroupCode == groupCode)
                list.Add(new StudentWithAvg(s, AverageCalculator.Calculate(s)));
        }

        list.Sort((a, b) =>
            string.Compare(a.Student.Name, b.Student.Name, StringComparison.Ordinal));

        return list;
    }


    public Statistics GetStatistics()
    {
        var students = _repository.GetAllStudents();
        return new Statistics(
            TotalStudents: students.Count,
            TotalGrades:   students.Sum(s => s.Grades.Count),
            MeanOfMeans:   students.Average(s => AverageCalculator.Calculate(s)),
            MaxGrade:      students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max(),
            HasFailing:    students.Any(s => s.Grades.Any(g => g < 5)),
            AllHaveEmail:  students.All(s => !string.IsNullOrWhiteSpace(s.Email))
        );
    }

    public Statistics GetStatisticsWithoutLinq()
    {
        var students = _repository.GetAllStudents();

        int    totalStudents = students.Count;
        int    totalGrades   = 0;
        double sumOfAverages = 0.0;
        int    maxGrade      = 0;
        bool   hasFailing    = false;
        bool   allHaveEmail  = true;

        foreach (var s in students)
        {
            totalGrades   += s.Grades.Count;
            sumOfAverages += AverageCalculator.Calculate(s);

            if (string.IsNullOrWhiteSpace(s.Email))
                allHaveEmail = false;

            foreach (var g in s.Grades)
            {
                if (g > maxGrade) maxGrade = g;
                if (g < 5)        hasFailing = true;
            }
        }

        double meanOfMeans = totalStudents == 0 ? 0.0 : sumOfAverages / totalStudents;

        return new Statistics(
            TotalStudents: totalStudents,
            TotalGrades:   totalGrades,
            MeanOfMeans:   meanOfMeans,
            MaxGrade:      maxGrade,
            HasFailing:    hasFailing,
            AllHaveEmail:  allHaveEmail
        );
    }
}
