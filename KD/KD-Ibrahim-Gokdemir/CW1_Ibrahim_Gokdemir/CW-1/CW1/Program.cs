using System;
using System.Linq;
using CW1.Interfaces;
using CW1.Services;
using CW1.UI;

namespace CW1;

class Program
{
    static void Main(string[] args)
    {
        // 1) parse args
        bool useStub = args.Contains("--stub", StringComparer.OrdinalIgnoreCase);

        // 2) conditional injection — the only place in the whole program that
        //    decides which repository implementation to use.
        IStudentRepository repository = useStub
            ? new StubStudentRepository()
            : new MemoryStudentRepository();

        // 3) tell the user which repository is in use (Task 3.3)
        if (useStub)
            Console.WriteLine("[INFO] Using StubStudentRepository (--stub).");
        else
            Console.WriteLine("[INFO] Using MemoryStudentRepository (default).");

        // 4) composition root — wire up the dependency chain.
        //    A single IStudentRepository instance lives for the whole run.
        StudentService studentService = new StudentService(repository);
        ReportService reportService = new ReportService(studentService);
        ConsoleMenu menu = new ConsoleMenu(studentService, reportService);

        menu.Show();
    }
}
