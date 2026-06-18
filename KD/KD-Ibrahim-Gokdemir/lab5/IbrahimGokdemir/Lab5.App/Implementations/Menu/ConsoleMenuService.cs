public class ConsoleMenuService : IMenuService
{
    private readonly StudentService _studentService;

    public ConsoleMenuService(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        var group = _studentService.BuildValidatedGroup("Console Group");
        _studentService.PrintStudents(group);
        _studentService.PrintAverages(group);
    }
}