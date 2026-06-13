using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Models;

public class DebugMenuService : IMenuService
{
    private readonly StudentService _service;

    public DebugMenuService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("DEBUG MODE");

        // Create a test group
        Group g = new Group();
        g.AddStudent(new Student(1, "Alice", "alice@test.com"));
        g.AddStudent(new Student(2, "Bob", "bob@test.com"));

        _service.PrintGroup(g);
    }
}
