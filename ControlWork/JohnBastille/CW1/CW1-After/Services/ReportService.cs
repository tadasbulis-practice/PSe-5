
using System;
using System.Collections.Generic;
using System.Linq;
using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;
public class ReportService
{
    private readonly IStudentRepository _repo;
    private readonly AverageCalculator _avgCalc;

    public ReportService(IStudentRepository repo, AverageCalculator avgCalc)
    {
        _repo = repo;
        _avgCalc = avgCalc;
    }

    public IEnumerable<(Student Student, double Avg)> Top3ByAverage_Linq()
    {
        return _repo.GetAll()
                    .Select(s => (Student: s, Avg: _avgCalc.Calculate(s.Grades)))
                    .OrderByDescending(x => x.Avg)
                    .Take(3);
    }

    public IEnumerable<(Student Student, double Avg)> Top3ByAverage_NoLinq()
    {
        var list = new List<(Student Student, double Avg)>();
        foreach (var s in _repo.GetAll())
            list.Add((s, _avgCalc.Calculate(s.Grades)));

        for (int i = 0; i < list.Count - 1; i++)
            for (int j = i + 1; j < list.Count; j++)
                if (list[j].Avg > list[i].Avg)
                {
                    var tmp = list[i];
                    list[i] = list[j];
                    list[j] = tmp;
                }

        var res = new List<(Student, double)>();
        for (int k = 0; k < Math.Min(3, list.Count); k++) res.Add(list[k]);
        return res;
    }

    public IEnumerable<Student> StudentsInGroup_Linq(string group)
    {
        return _repo.GetAll().Where(s => s.GroupCode == group).OrderBy(s => s.Name);
    }

    public IEnumerable<Student> StudentsInGroup_NoLinq(string group)
    {
        var matches = new List<Student>();
        foreach (var s in _repo.GetAll())
            if (s.GroupCode == group) matches.Add(s);

        for (int i = 1; i < matches.Count; i++)
        {
            var key = matches[i];
            int j = i - 1;
            while (j >= 0 && string.Compare(matches[j].Name, key.Name, StringComparison.Ordinal) > 0)
            {
                matches[j + 1] = matches[j];
                j--;
            }
            matches[j + 1] = key;
        }
        return matches;
    }

    public (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_Linq()
    {
        var students = _repo.GetAll().ToList();
        int totalStudents = students.Count;
        int totalGrades = students.Sum(s => s.Grades.Count);
        double meanOfMeans = students.Count == 0 ? 0.0 : students.Average(s => _avgCalc.Calculate(s.Grades));
        int maxGrade = students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max();
        bool hasFailing = students.Any(s => s.Grades.Any(g => g < 5));
        bool allHaveEmail = students.All(s => !string.IsNullOrWhiteSpace(s.Email));
        return (totalStudents, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }

    public (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_NoLinq()
    {
        var students = new List<Student>(_repo.GetAll());
        int totalStudents = 0;
        int totalGrades = 0;
        double sumOfAverages = 0.0;
        int maxGrade = 0;
        bool hasFailing = false;
        bool allHaveEmail = true;

        foreach (var s in students)
        {
            totalStudents++;
            int cnt = 0;
            int sum = 0;
            foreach (var g in s.Grades)
            {
                sum += g;
                cnt++;
                if (g > maxGrade) maxGrade = g;
                if (g < 5) hasFailing = true;
            }
            totalGrades += cnt;
            double avg = cnt == 0 ? 0.0 : (double)sum / cnt;
            sumOfAverages += avg;
            if (string.IsNullOrWhiteSpace(s.Email)) allHaveEmail = false;
        }

        double meanOfAverages = totalStudents == 0 ? 0.0 : sumOfAverages / totalStudents;
        return (totalStudents, totalGrades, meanOfAverages, maxGrade, hasFailing, allHaveEmail);
    }
}