using CW1.Interfaces;
using CW1.Services;
using CW1.UI;

namespace CW1;

class Program
{
    static void Main(string[] args)
    {

        bool useStub = args.Any(a => a == "--stub");

        IStudentRepository repository = useStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        // ─────────────────────────────────────────────────────────────
        // Services (all dependencies injected via constructor)
        // ─────────────────────────────────────────────────────────────
        var studentService = new StudentService(repository);
        var calculator = new AverageCalculator();
        var validator = new StudentValidator();
        var reportService = new ReportService(repository, calculator);

        // ─────────────────────────────────────────────────────────────
        // UI layer
        // ─────────────────────────────────────────────────────────────
        var menu = new ConsoleMenu(
            studentService,
            reportService,
            validator,
            calculator
        );

        // ─────────────────────────────────────────────────────────────
        // Startup info (Task 3 requirement)
        // ─────────────────────────────────────────────────────────────
        const string modeStub = "[INFO] Using StubStudentRepository (--stub).";
        const string modeMem = "[INFO] Using MemoryStudentRepository (default).";

        Console.WriteLine(useStub ? modeStub : modeMem);

        menu.Run();
    }
}