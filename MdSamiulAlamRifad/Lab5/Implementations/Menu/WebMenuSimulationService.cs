using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Models;

public class WebMenuSimulationService : IMenuService
{
    private readonly StudentService _service;

    public WebMenuSimulationService(StudentService service)
    {
        _service = service;
    }

    public void Run()
    {
        Console.WriteLine("Simulating web request...");

        Group g = new Group();
        g.AddStudent(new Student(1, "Alice", "alice@test.com"));
        g.AddStudent(new Student(2, "Bob", "bob@test.com"));

        _service.PrintGroup(g);
    }
}
