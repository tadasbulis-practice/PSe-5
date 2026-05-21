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

        if (useStub)
        {
            Console.WriteLine(
                "[INFO] Using StubStudentRepository (--stub).");
        }
        else
        {
            Console.WriteLine(
                "[INFO] Using MemoryStudentRepository (default).");
        }

        AverageCalculator calculator = new();

        StudentValidator validator = new();

        StudentService studentService =
            new StudentService(
                repository,
                validator);

        ReportService reportService =
            new ReportService(calculator);

        ConsoleMenu menu =
            new ConsoleMenu(
                studentService,
                calculator,
                reportService);

        menu.Start();
    }
}