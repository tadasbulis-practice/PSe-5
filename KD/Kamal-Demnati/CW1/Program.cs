using CW1.Interfaces;
using CW1.Services;
using CW1.UI;

namespace CW1;

public enum QueryMode
{
    Linq,
    NoLinq
}

class Program
{
    static void Main(string[] args)
    {
        bool useStub = args.Any(a => a == "--stub");

        bool useLinq = args.Any(a => a == "--linq");
        bool useNoLinq = args.Any(a => a == "--nolinq");

        if (useLinq && useNoLinq)
        {
            Console.WriteLine("[ERROR] Cannot use both --linq and --nolinq");
            return;
        }

        QueryMode mode =
            useNoLinq ? QueryMode.NoLinq :
            useLinq ? QueryMode.Linq :
            QueryMode.Linq; // default


        IStudentRepository repository = useStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        // Services
        var studentService = new StudentService(repository);
        var calculator = new AverageCalculator();
        var validator = new StudentValidator();
        var reportService = new ReportService(repository, calculator);

        // UI
        var menu = new ConsoleMenu(
            studentService,
            reportService,
            validator,
            calculator,
            mode
        );

        // Startup info
        Console.WriteLine(
            useStub
                ? "[INFO] Using StubStudentRepository (--stub)."
                : "[INFO] Using MemoryStudentRepository (default)."
        );

        Console.WriteLine(
            mode == QueryMode.Linq
                ? "[INFO] LINQ mode enabled (default)."
                : "[INFO] NO LINQ mode enabled (--nolinq)."
        );

        menu.Run();
    }
}