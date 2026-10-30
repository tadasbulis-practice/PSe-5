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
        Console.WriteLine("LAB-6 live coding result: from single object to collection system.");

        var alice = new Student(1, "Alice", "alice@test.com");
        alice.AddGrade(9);
        alice.AddGrade(8);

        var bob = new Student(2, "Bob", "bob@test.com");
        bob.AddGrade(7);
        bob.AddGrade(10);

        var invalidStudent = new Student(3, "Broken Email", "broken-email");
        invalidStudent.AddGrade(5);

        _service.AddStudent(alice);
        _service.AddStudent(bob);
        _service.AddStudent(invalidStudent);

        _service.PrintAllStudents();

        Console.WriteLine();
        Console.WriteLine($"Group average: {_service.CalculateGroupAverage():0.00}");

        var found = _service.FindStudentById(2);
        Console.WriteLine(found is null
            ? "Student not found"
            : $"Found: {found.Name}");

        _service.RemoveStudent(1);

        Console.WriteLine();
        Console.WriteLine("After removing student with ID = 1:");
        _service.PrintAllStudents();
    }
}
