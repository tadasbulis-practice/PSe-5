using CW1After.Interfaces;
using CW1After.Models;
using System.Linq;

namespace CW1After.Services;

// Small result types so the report computes data and the UI does the printing.
public record StudentAverage(Student Student, double Average);

public record Statistics(
    int TotalStudents,
    int TotalGrades,
    double MeanOfAverages,
    int MaxGrade,
    bool HasFailing,
    bool AllHaveEmail);

public class ReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _calculator;

    public ReportService(IStudentRepository repository, AverageCalculator calculator)
    {
        _repository = repository;
        _calculator = calculator;
    }

    // ============================================================
    // 7) Top N students by average
    // ============================================================
    public List<StudentAverage> GetTopByAverage(int n)
    {
        return _repository.GetAllStudents()
            .Select(s => new StudentAverage(s, _calculator.Average(s)))
            .OrderByDescending(x => x.Average)
            .Take(n)
            .ToList();
    }

    // ============================================================
    // 8) Students in a group, sorted by name
    // ============================================================
    public List<Student> GetStudentsInGroupSortedByName(string code)
    {
        return _repository.GetAllStudents()
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.Name)
            .ToList();
    }

    // ============================================================
    // 9) Statistics
    // ============================================================
    public Statistics GetStatistics()
    {
        var students = _repository.GetAllStudents();

        int totalStudents = students.Count;
        int totalGrades = students.Sum(s => s.Grades.Count);
        double meanOfAverages = students.Count == 0
            ? 0.0
            : students.Average(s => _calculator.Average(s));
        int maxGrade = students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
        bool hasFailing = students.Any(s => s.Grades.Any(g => g < 5));
        bool allHaveEmail = students.All(s => !string.IsNullOrWhiteSpace(s.Email));

        return new Statistics(
            totalStudents, totalGrades, meanOfAverages,
            maxGrade, hasFailing, allHaveEmail);
    }

    // ============================================================
    // 7) Top N by average — WITHOUT LINQ
    // ============================================================
    public List<StudentAverage> GetTopByAverageWithoutLinq(int n)
    {
        // Build the (student, average) pairs with a plain loop.
        var pairs = new List<StudentAverage>();
        foreach (var s in _repository.GetAllStudents())
            pairs.Add(new StudentAverage(s, _calculator.Average(s)));

        // Sort by average descending using a Comparison delegate.
        pairs.Sort((a, b) => b.Average.CompareTo(a.Average));

        // Take the first n (or fewer if the list is smaller).
        var result = new List<StudentAverage>();
        for (int i = 0; i < pairs.Count && i < n; i++)
            result.Add(pairs[i]);

        return result;
    }

    // ============================================================
    // 8) Students in a group, sorted by name — WITHOUT LINQ
    // ============================================================
    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string code)
    {
        // Filter by group code with a foreach + if.
        var filtered = new List<Student>();
        foreach (var s in _repository.GetAllStudents())
            if (s.GroupCode == code)
                filtered.Add(s);

        // Sort by name ascending (ordinal) using a Comparison delegate.
        filtered.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

        return filtered;
    }

    // ============================================================
    // 9) Statistics — WITHOUT LINQ
    // ============================================================
    public Statistics GetStatisticsWithoutLinq()
    {
        var students = _repository.GetAllStudents();

        int totalStudents = students.Count;   // .Count is the property — allowed.

        int totalGrades = 0;
        foreach (var s in students)
            totalGrades += s.Grades.Count;

        // Mean of each student's average.
        double sumOfAverages = 0.0;
        foreach (var s in students)
            sumOfAverages += _calculator.Average(s);
        double meanOfAverages = totalStudents == 0 ? 0.0 : sumOfAverages / totalStudents;

        // Max grade across all students (default 0 if no grades exist).
        int maxGrade = 0;
        bool foundAnyGrade = false;
        foreach (var s in students)
        {
            foreach (var g in s.Grades)
            {
                if (!foundAnyGrade || g > maxGrade)
                {
                    maxGrade = g;
                    foundAnyGrade = true;
                }
            }
        }

        // Any failing grade (< 5)?
        bool hasFailing = false;
        foreach (var s in students)
        {
            foreach (var g in s.Grades)
            {
                if (g < 5) { hasFailing = true; break; }
            }
            if (hasFailing) break;
        }

        // Do all students have an email?
        bool allHaveEmail = true;
        foreach (var s in students)
        {
            if (string.IsNullOrWhiteSpace(s.Email)) { allHaveEmail = false; break; }
        }

        return new Statistics(
            totalStudents, totalGrades, meanOfAverages,
            maxGrade, hasFailing, allHaveEmail);
    }
}