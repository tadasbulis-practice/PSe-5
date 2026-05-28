using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class ReportService
{
    private readonly IStudentRepository repository;
    private readonly AverageCalculator calculator;

    public ReportService(
        IStudentRepository repository,
        AverageCalculator calculator)
    {
        this.repository = repository;
        this.calculator = calculator;
    }

    // LINQ version
    public List<Student> GetTopByAverage(int n)
    {
        return repository.GetStudents()
            .OrderByDescending(
                s => calculator.Calculate(s)) // lambda function to calculate the s : student object return avrage
            .Take(n) // limit for the n number 
            .ToList(); // create the query (iterated through the list , transform object to list )
    }

    // NO LINQ version
    public List<Student> GetTopByAverageWithoutLinq(int n)
    {
        List<Student> copy = new();

        foreach (var s in repository.GetStudents())
        {
            copy.Add(s);
        }

        copy.Sort((a, b) =>
            calculator.Calculate(b)
            .CompareTo(calculator.Calculate(a)));

        List<Student> result = new();

        for (int i = 0; i < n && i < copy.Count; i++)
        {
            result.Add(copy[i]);
        }

        return result;
    }

    // LINQ version
    public List<Student> GetStudentsInGroupSortedByName(string code)
    {
        return repository.GetStudents() // call repository that impliment IStudentRepository return Student objects 
            .Where(s => s.GroupCode == code)    // lambda function that return the students objects that hold code var
            .OrderBy(s => s.Name) // sorts the sequence in descending order.
            .ToList();
    }

    // NO LINQ version
    public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string code)
    {
        List<Student> result = new();

        foreach (var s in repository.GetStudents())
        {
            if (s.GroupCode == code)
            {
                result.Add(s);
            }
        }

        result.Sort((a, b) => a.Name.CompareTo(b.Name));

        return result;
    }


    //  LINQ version
    public (int count, int sum, double avg, int max, bool any, bool all)
    GetStatistics()
    {
        var students = repository.GetStudents();

        int count = students.Count;
        int sum = students.Sum(s => s.Grades.Count);
        double avg = students.Count == 0
            ? 0
            : students.Average(s => calculator.Calculate(s));

        int max = students
            .SelectMany(s => s.Grades)
            .DefaultIfEmpty(0)
            .Max();

        bool any = students.Any(s => s.Grades.Any(g => g < 5));
        bool all = students.All(s => s.Grades.Count > 0);

        return (count, sum, avg, max, any, all);
    }



    // NO LINQ version
    public (int count, int sum, double avg, int max, bool any, bool all)
        GetStatisticsWithoutLinq()
    {
        var students = repository.GetStudents();

        int count = students.Count;
        int sum = 0;
        int max = int.MinValue;
        bool any = false;
        bool all = true;

        double totalAverages = 0;
        double avg = 0;

        foreach (var s in students)
        {
            if (s.Grades.Count == 0)
            {
                all = false;
            }

            int localSum = 0;

            foreach (var g in s.Grades)
            {
                sum++;
                localSum += g;

                if (g < 5)
                    any = true;

                if (g > max)
                    max = g;
            }

            totalAverages += calculator.Calculate(s);
        }

        avg = count == 0 ? 0 : totalAverages / (double)count;

        return (count, sum, avg, max, any, all);
    }
}