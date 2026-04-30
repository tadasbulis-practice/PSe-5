using Lab4Demo.Interfaces;
using Lab4Demo.Models;

namespace Lab4Demo.Services;

public class StudentService
{
    private readonly IStudentRepository _repo;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _average;

    public StudentService(
        IStudentRepository repo,
        IStudentPrinter printer,
        IAverageStrategy average)
    {
        _repo = repo;
        _printer = printer;
        _average = average;
    }

    public void Run()
    {
        var students = _repo.GetAll();

        foreach (var s in students)
        {

            double avg = _average.CalculateAverage(s.Grades);
            Console.WriteLine($"Average: {avg}");

            _printer.Print(s);
        }
    }
}

