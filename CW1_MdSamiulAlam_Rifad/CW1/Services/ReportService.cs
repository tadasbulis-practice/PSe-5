using CW1After.Interfaces;
using CW1After.Models;
using CW1After.Services;

namespace CW1After.Services;

public class Statistics
{
    public int Count { get; set; }
    public double Sum { get; set; }
    public double Average { get; set; }
    public double Max { get; set; }
    public bool AnyAbove5 { get; set; }
    public bool AllHaveGrades { get; set; }
}

public class ReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageCalculator _averageCalculator;

    public ReportService(IStudentRepository repository, AverageCalculator averageCalculator)
    {
        _repository = repository;
        _averageCalculator = averageCalculator;
    }

    // ─────────────────────────────────────────────
    //  Menu item 7 — Top N students by average
    // ─────────────────────────────────────────────

    // WITH LINQ
    public List<Student> GetTopByAverage(int n)
    {
        return _repository.GetAllStudents()
            .OrderByDescending(s => _averageCalculator.Calculate(s))
            .Take(n)
            .ToList();
    }

    // WITHOUT LINQ
    public List<Student> GetTopByAverageWithoutLinq(int n)
    {
        // Copy the list so we don't mutate the original
        var copy = new List<Student>(_repository.GetAllStudents());
        var result = new List<Student>();

        int take = n < copy.Count ? n : copy.Count;

        for (int i = 0; i < take; i++)
        {
            // Find the student with the highest average in what remains
            int bestIndex = 0;
            double bestAvg = _averageCalculator.Calculate(copy[0]);

            for (int j = 1; j < copy.Count; j++)
            {
                double avg = _averageCalculator.Calculate(copy[j]);
                if (avg > bestAvg)
                {
                    bestAvg = avg;
                    bestIndex = j;
                }
            }

            result.Add(copy[bestIndex]);
            copy.RemoveAt(bestIndex);
        }

        return result;
    }

    // ─────────────────────────────────────────────
    //  Menu item 8 — Students in a group, sorted by name
    // ─────────────────────────────────────────────

    // WITH LINQ
    public List<Student> GetStudentsInGroupSortedByName(string code)
    {
        return _repository.GetAllStudents()
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.Name)
            .ToList();
    }

    // WITHOUT LINQ
    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string code)
    {
        var filtered = new List<Student>();

        foreach (var s in _repository.GetAllStudents())
        {
            if (s.GroupCode == code)
                filtered.Add(s);
        }

        filtered.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        return filtered;
    }

    // ─────────────────────────────────────────────
    //  Menu item 9 — Statistics
    // ─────────────────────────────────────────────

    // WITH LINQ
    public Statistics GetStatistics()
    {
        var students = _repository.GetAllStudents();
        var averages = students.Select(s => _averageCalculator.Calculate(s)).ToList();

        return new Statistics
        {
            Count   = students.Count(),
            Sum     = averages.Sum(),
            Average = averages.Average(),
            Max     = averages.Max(),
            AnyAbove5   = averages.Any(a => a > 5),
            AllHaveGrades = students.All(s => s.Grades.Count > 0)
        };
    }

    // WITHOUT LINQ
    public Statistics GetStatisticsWithoutLinq()
    {
        var students = _repository.GetAllStudents();

        int count = students.Count;       // List<T>.Count property — allowed
        double sum = 0;
        double max = double.MinValue;
        bool anyAbove5 = false;
        bool allHaveGrades = true;

        for (int i = 0; i < students.Count; i++)
        {
            double avg = _averageCalculator.Calculate(students[i]);
            sum += avg;

            if (avg > max) max = avg;
            if (avg > 5)   anyAbove5 = true;
            if (students[i].Grades.Count == 0) allHaveGrades = false;
        }

        double average = count > 0 ? sum / count : 0;

        return new Statistics
        {
            Count         = count,
            Sum           = sum,
            Average       = average,
            Max           = max,
            AnyAbove5     = anyAbove5,
            AllHaveGrades = allHaveGrades
        };
    }
}
