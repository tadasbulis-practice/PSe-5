using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

namespace CW1After;

public static class Program
{
    public static void Main(string[] args)
    {
        // Optional: run the LINQ drill self-test and exit.
        if (args.Contains("--drills"))
        {
            DrillTests.RunAll();
            return;
        }

        // 1) Parse command-line arguments
        bool useStub = args.Contains("--stub");

        // 2) Conditional injection — choose the data source.
        //    Both implement IStudentRepository, so nothing downstream changes.
        IStudentRepository repository = useStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        // 3) Tell the user which repository is active
        if (useStub)
            Console.WriteLine("[INFO] Using StubStudentRepository (--stub).");
        else
            Console.WriteLine("[INFO] Using MemoryStudentRepository (default).");

        // 4) Build the rest of the object graph (constructor injection)
        var calculator = new AverageCalculator();
        var validator = new StudentValidator(repository);
        var service = new StudentService(repository, calculator, validator);
        var reports = new ReportService(repository, calculator);
        var menu = new ConsoleMenu(service, reports);

        // 5) Run
        menu.Run();
    }
}