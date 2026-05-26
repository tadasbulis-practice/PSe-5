using Lab6.Interfaces;
using Lab6.Models;
using Lab6.Services;

namespace Lab6.Implementations.Menu;

public class ConsoleMenuService : IMenuService
{
    private readonly StudentService _service;

    public ConsoleMenuService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("LAB-6 starter architecture running.");

        var group = new Group();
        var student = new Student(1, "Alice", "alice@test.com");
        _service.AddStudent(student);

        _service.AddGrade(student, 10);
        _service.AddGrade(student, 2);

        var student2 = new Student(2, "removed student", "alice@test.com");
        _service.AddStudent(student2);

        var studentsList = _service.GetAllStudents();
        _service.PrintStudents(studentsList);

        double avg = _service.CalculateAverage(student);
        Console.WriteLine($"Average: {avg}");

        _service.RemoveStudent(student2);
        studentsList = _service.GetAllStudents();
        _service.PrintStudents(studentsList);
    }
}
