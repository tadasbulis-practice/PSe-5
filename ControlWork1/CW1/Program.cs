

using CW1After.Interfaces;
using CW1After.Services;
using CW1After.UI;

namespace CW1After;

public static class Program
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

        var validator      = new StudentValidator(repository);
        var studentService = new StudentService(repository, validator);
        var reportService  = new ReportService(repository);
        var menu           = new ConsoleMenu(studentService, reportService);

        menu.Run();
    }
}
