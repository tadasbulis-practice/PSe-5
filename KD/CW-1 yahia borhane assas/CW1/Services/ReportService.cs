using CW1After.Models;

namespace CW1After.Services;

public class ReportService
{
    private readonly AverageCalculator calculator;

    public ReportService(AverageCalculator calculator)
    {
        this.calculator = calculator;
    }

    // =========================
    // LINQ VERSION
    // Top N students by average
    // =========================

    public List<Student> GetTopByAverage(
        List<Student> students,
        int n)
    {
        return students
            .OrderByDescending(
                s => calculator.Calculate(s.Grades))
            .Take(n)
            .ToList();
    }

    // =========================
    // WITHOUT LINQ VERSION
    // Top N students by average
    // =========================

    public List<Student> GetTopByAverageWithoutLinq(
        List<Student> students,
        int n)
    {
        List<Student> result = new();

        foreach (Student student in students)
        {
            result.Add(student);
        }

        result.Sort((a, b) =>
            calculator.Calculate(b.Grades)
            .CompareTo(
                calculator.Calculate(a.Grades)));

        List<Student> top = new();

        for (int i = 0; i < n && i < result.Count; i++)
        {
            top.Add(result[i]);
        }

        return top;
    }

    // =========================
    // LINQ VERSION
    // Students in group sorted by name
    // =========================

    public List<Student> GetStudentsInGroupSortedByName(
        List<Student> students,
        string code)
    {
        return students
            .Where(s => s.GroupCode == code)
            .OrderBy(s => s.Name)
            .ToList();
    }

    // =========================
    // WITHOUT LINQ VERSION
    // Students in group sorted by name
    // =========================

    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(
        List<Student> students,
        string code)
    {
        List<Student> result = new();

        foreach (Student student in students)
        {
            if (student.GroupCode == code)
            {
                result.Add(student);
            }
        }

        result.Sort((a, b) =>
            a.Name.CompareTo(b.Name));

        return result;
    }

    // =========================
    // LINQ VERSION
    // Statistics
    // =========================

    public void GetStatistics(List<Student> students)
    {
        int count = students.Count();

        double sum = students
            .Sum(s => calculator.Calculate(s.Grades));

        double average = students
            .Average(s => calculator.Calculate(s.Grades));

        double max = students
            .Max(s => calculator.Calculate(s.Grades));

        bool any = students
            .Any(s => calculator.Calculate(s.Grades) > 9);

        bool all = students
            .All(s => calculator.Calculate(s.Grades) >= 5);

        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Max: {max}");
        Console.WriteLine($"Any > 9: {any}");
        Console.WriteLine($"All >= 5: {all}");
    }

    // =========================
    // WITHOUT LINQ VERSION
    // Statistics
    // =========================

    public void GetStatisticsWithoutLinq(
        List<Student> students)
    {
        int count = 0;

        double sum = 0;

        double max = 0;

        bool any = false;

        bool all = true;

        foreach (Student student in students)
        {
            double avg =
                calculator.Calculate(student.Grades);

            count++;

            sum += avg;

            if (avg > max)
            {
                max = avg;
            }

            if (avg > 9)
            {
                any = true;
            }

            if (avg < 5)
            {
                all = false;
            }
        }

        double average = sum / count;

        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Max: {max}");
        Console.WriteLine($"Any > 9: {any}");
        Console.WriteLine($"All >= 5: {all}");
    }
}