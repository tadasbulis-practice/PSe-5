using CW1Friend.Interfaces;
using CW1Friend.Models;

namespace CW1Friend.Services;

public class GroupStatistics
{
    public int TotalStudents   { get; set; }
    public double SumOfAverages { get; set; }
    public double OverallAverage { get; set; }
    public double HighestAverage { get; set; }
    public bool SomeoneAbove5  { get; set; }
    public bool EveryoneGraded { get; set; }
}

public class ReportService
{
    private readonly IStudentRepository _repo;
    private readonly AverageCalculator _calc;

    public ReportService(IStudentRepository repo, AverageCalculator calc)
    {
        _repo = repo;
        _calc = calc;
    }

    // ── Menu 7: Top N by average ──────────────────────────────

    public List<Student> TopStudents(int n)
    {
        return _repo.FetchAllStudents()
            .OrderByDescending(s => _calc.GetAverage(s))
            .Take(n)
            .ToList();
    }

    public List<Student> TopStudentsNoLinq(int n)
    {
        var pool   = new List<Student>(_repo.FetchAllStudents());
        var result = new List<Student>();

        int limit = n < pool.Count ? n : pool.Count;

        for (int i = 0; i < limit; i++)
        {
            int topIdx = 0;
            double topAvg = _calc.GetAverage(pool[0]);

            for (int j = 1; j < pool.Count; j++)
            {
                double a = _calc.GetAverage(pool[j]);
                if (a > topAvg)
                {
                    topAvg = a;
                    topIdx = j;
                }
            }

            result.Add(pool[topIdx]);
            pool.RemoveAt(topIdx);
        }

        return result;
    }

    // ── Menu 8: Students in group sorted by name ─────────────

    public List<Student> ByGroupSorted(string code)
    {
        return _repo.FetchAllStudents()
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.FullName)
            .ToList();
    }

    public List<Student> ByGroupSortedNoLinq(string code)
    {
        var filtered = new List<Student>();

        foreach (Student s in _repo.FetchAllStudents())
        {
            if (s.GroupCode == code)
                filtered.Add(s);
        }

        filtered.Sort((x, y) => string.Compare(x.FullName, y.FullName, StringComparison.Ordinal));

        return filtered;
    }

    // ── Menu 9: Statistics ────────────────────────────────────

    public GroupStatistics BuildStats()
    {
        var all  = _repo.FetchAllStudents();
        var avgs = all.Select(s => _calc.GetAverage(s)).ToList();

        return new GroupStatistics
        {
            TotalStudents   = all.Count(),
            SumOfAverages   = avgs.Sum(),
            OverallAverage  = avgs.Average(),
            HighestAverage  = avgs.Max(),
            SomeoneAbove5   = avgs.Any(a => a > 5),
            EveryoneGraded  = all.All(s => s.Grades.Count > 0)
        };
    }

    public GroupStatistics BuildStatsNoLinq()
    {
        var all    = _repo.FetchAllStudents();
        int count  = all.Count;
        double sum = 0;
        double max = double.MinValue;
        bool someAbove5    = false;
        bool allGraded     = true;

        for (int i = 0; i < all.Count; i++)
        {
            double avg = _calc.GetAverage(all[i]);
            sum += avg;
            if (avg > max)  max = avg;
            if (avg > 5)    someAbove5 = true;
            if (all[i].Grades.Count == 0) allGraded = false;
        }

        return new GroupStatistics
        {
            TotalStudents   = count,
            SumOfAverages   = sum,
            OverallAverage  = count > 0 ? sum / count : 0,
            HighestAverage  = max,
            SomeoneAbove5   = someAbove5,
            EveryoneGraded  = allGraded
        };
    }
}
