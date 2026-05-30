using Lab5.Interfaces;
using Lab5.Models;
using Lab5.Services;

namespace Lab5.Implementations.Menu;

public class ConsoleMenuService : IMenuService
{
    private readonly StudentService _service;

    public ConsoleMenuService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("LAB-5 starter architecture running.");

        var group = new Group();
        var student = new Student(1, "Alice", "alice@test.com");
        student.AddGrade(9);
        student.AddGrade(8);
        student.AddGrade(7);
        student.AddGrade(12);

        group.AddStudent(student);

        _service.PrintGroup(group);

        double avg = _service.CalculateAverage(student);
        Console.WriteLine($"Average: {avg}");
    }
}
