using System;

public class WebMenuSimulationService : IMenuService
{
    private readonly StudentService _studentService;

    public WebMenuSimulationService(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        Console.WriteLine("Simulating web request...");
        var group = _studentService.BuildValidatedGroup("Web Group");
        _studentService.PrintStudents(group);
        _studentService.PrintAverages(group);
        Console.WriteLine("Web response completed.");
    }
}