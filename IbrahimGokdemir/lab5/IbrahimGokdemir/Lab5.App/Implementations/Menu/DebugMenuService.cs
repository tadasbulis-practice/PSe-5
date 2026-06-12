using System;

public class DebugMenuService : IMenuService
{
    private readonly StudentService _studentService;

    public DebugMenuService(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        Console.WriteLine("DEBUG MODE STARTED");
        var group = _studentService.BuildValidatedGroup("Debug Group");
        _studentService.PrintStudents(group);
        _studentService.PrintAverages(group);
        Console.WriteLine("DEBUG MODE FINISHED");
    }
}