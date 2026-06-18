using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _strategy;
    private readonly IStudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IAverageStrategy strategy,
        IStudentValidator validator)
    {
        _repository = repository;
        _printer = printer;
        _strategy = strategy;
        _validator = validator;
    }

    public void Run()
    {
        // Same pipeline as Lab-6: GetAll -> ValidateAll -> Calculate -> Print.
        // Only the repository underneath changed (Memory or Api).
        var students = _repository.GetAll();
        var validStudents = _validator.ValidateAll(students);

        _printer.PrintStudents(validStudents);

        var avg = _strategy.Calculate(validStudents);
        Console.WriteLine($"\nGroup Average: {avg:0.00}");

        PrintGroupReport(validStudents);
        PrintTopStudent(validStudents);
    }

    // LINQ: GroupBy + Average — per-group statistics (slide 19 pattern)
    private void PrintGroupReport(IReadOnlyList<Student> students)
    {
        var groups = students
            .Where(s => s.GroupCode != null)
            .GroupBy(s => s.GroupCode)
            .OrderBy(g => g.Key)
            .ToList();

        if (!groups.Any())
        {
            return;
        }

        Console.WriteLine("\nGroup Report:");
        foreach (var group in groups)
        {
            var groupAvg = group.SelectMany(s => s.Grades).DefaultIfEmpty(0).Average();
            Console.WriteLine($"  {group.Key}: {group.Count()} student(s), average {groupAvg:0.00}");
        }
    }

    // LINQ: OrderByDescending + FirstOrDefault — safe "best of" lookup (slide 18 pattern)
    private void PrintTopStudent(IReadOnlyList<Student> students)
    {
        var top = students
            .OrderByDescending(s => s.Grades.DefaultIfEmpty(0).Average())
            .FirstOrDefault();

        if (top != null)
        {
            var topAvg = top.Grades.DefaultIfEmpty(0).Average();
            Console.WriteLine($"\nTop student: {top.Name} ({topAvg:0.00})");
        }
    }
}
