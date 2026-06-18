using System;
using System.Collections.Generic;
using System.Linq;
using CW1.Models;

namespace CW1.Services;

public class ReportService
{
    private readonly StudentService _studentService;

    // Constructor injection: ReportService only depends on StudentService,
    // it never touches Console and never talks to the repository directly.
    public ReportService(StudentService studentService)
    {
        _studentService = studentService;
    }

    // =========================================================================
    // MENU ITEM: Top N students by average
    // GetTopByAverage(n)              -> LINQ version
    // GetTopByAverageWithoutLinq(n)   -> for/foreach/if + List<T>.Sort only
    // =========================================================================

    public List<Student> GetTopByAverage(int n)
    {
        var students = _studentService.GetAllStudents();
        return students
            .OrderByDescending(s => _studentService.CalculateAverage(s.Grades))
            .Take(n)
            .ToList();
    }

    public List<Student> GetTopByAverageWithoutLinq(int n)
    {
        var students = _studentService.GetAllStudents();

        List<Student> sorted = new List<Student>(students);
        sorted.Sort((a, b) =>
            _studentService.CalculateAverage(b.Grades).CompareTo(_studentService.CalculateAverage(a.Grades)));

        List<Student> result = new List<Student>();
        int limit = n < sorted.Count ? n : sorted.Count;
        for (int i = 0; i < limit; i++)
        {
            result.Add(sorted[i]);
        }
        return result;
    }

    // =========================================================================
    // MENU ITEM: Students in a group, sorted by name
    // GetStudentsInGroupSortedByName(code)            -> LINQ version
    // GetStudentsInGroupSortedByNameWithoutLinq(code) -> for/foreach/if + List<T>.Sort only
    // =========================================================================

    public List<Student> GetStudentsInGroupSortedByName(string groupCode)
    {
        var students = _studentService.GetAllStudents();
        return students
            .Where(s => s.GroupCode.Equals(groupCode, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s.Name)
            .ToList();
    }

    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string groupCode)
    {
        var students = _studentService.GetAllStudents();

        List<Student> result = new List<Student>();
        foreach (var s in students)
        {
            if (s.GroupCode.Equals(groupCode, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(s);
            }
        }

        result.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
        return result;
    }

    // =========================================================================
    // MENU ITEM: Statistics (Count / Sum / Average / Max / Any / All)
    // GetStatistics()            -> LINQ version
    // GetStatisticsWithoutLinq() -> for/foreach/if only
    // =========================================================================

    public (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailingGrade, bool AllHaveEmail) GetStatistics()
    {
        var students = _studentService.GetAllStudents();

        int totalStudents  = students.Count;
        int totalGrades    = students.Sum(s => s.Grades.Count);
        double meanOfMeans = students.Count == 0
            ? 0.0
            : students.Average(s => _studentService.CalculateAverage(s.Grades));
        int maxGrade      = students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
        bool hasFailing   = students.Any(s => s.Grades.Any(g => g < 5));
        bool allHaveEmail = students.All(s => !string.IsNullOrWhiteSpace(s.Email));

        return (totalStudents, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }

    public (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailingGrade, bool AllHaveEmail) GetStatisticsWithoutLinq()
    {
        var students = _studentService.GetAllStudents();

        int totalStudents = students.Count;
        int totalGrades = 0;
        double sumOfAverages = 0.0;
        int maxGrade = 0;
        bool hasFailing = false;
        bool allHaveEmail = true;

        foreach (var s in students)
        {
            totalGrades += s.Grades.Count;
            sumOfAverages += _studentService.CalculateAverage(s.Grades);

            foreach (var g in s.Grades)
            {
                if (g > maxGrade) maxGrade = g;
                if (g < 5) hasFailing = true;
            }

            if (string.IsNullOrWhiteSpace(s.Email))
            {
                allHaveEmail = false;
            }
        }

        double meanOfMeans = totalStudents == 0 ? 0.0 : sumOfAverages / totalStudents;

        return (totalStudents, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }
}
