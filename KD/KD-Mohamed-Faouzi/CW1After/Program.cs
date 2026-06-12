using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

namespace CW1After;

class Program
{
    static void Main(string[] args)
    {
        bool useStub =
            args.Contains("--stub");

        IStudentRepository repository =
            useStub
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

        var averageCalculator =
            new AverageCalculator();

        var validator =
            new StudentValidator();

        var studentService =
            new StudentService(
                repository,
                averageCalculator,
                validator);

        var reportService =
            new ReportService(
                repository,
                averageCalculator);

        var menu =
            new ConsoleMenu(
                studentService,
                reportService);

        menu.Run();
    }
}