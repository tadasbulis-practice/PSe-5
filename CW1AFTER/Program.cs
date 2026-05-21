using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

namespace CW1After;

public class Program
{
    public static void Main(string[] args)
    {
        bool useStub = args.Contains("--stub");
        IStudentRepository repository = useStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        Console.WriteLine(useStub
            ? "[INFO] Using StubStudentRepository (--stub)."
            : "[INFO] Using MemoryStudentRepository (default).");

        var calculator = new AverageCalculator();
        var validator = new StudentValidator();
        var studentService = new StudentService(repository, validator, calculator);
        var reportService = new ReportService(repository, calculator);
        var menu = new ConsoleMenu(studentService, reportService);

        menu.Run();
    }
}