using Lab8.Implementations.Adapter;
using Lab8.Implementations.Menu;
using Lab8.Implementations.Printer;
using Lab8.Implementations.Repository;
using Lab8.Implementations.Strategy;
using Lab8.Interfaces;
using Lab8.Services;

namespace Lab8;

class Program
{
    static void Main(string[] args)
    {
        // ══════════════════════════════════════════════════════════════
        // COMPOSITION ROOT — the only place where concrete classes are
        // chosen. Everything else uses interfaces.
        //
        // SOLID — Dependency Inversion Principle (DIP):
        // All dependencies are injected here. High-level modules
        // (StudentService, SolidDemoService) depend only on interfaces.
        //
        // To switch from demo data → real API:
        //   1. Start Docker:  docker compose up
        //   2. Change useApi to true below
        // ══════════════════════════════════════════════════════════════

        const string apiUrl = "http://localhost:6001";
        const bool   useApi = false;  // ← set true when Docker is running

        IStudentRepository repository = useApi
            ? new ApiStudentRepository(apiUrl)
            : new MemoryStudentRepository();

        IStudentPrinter   printer   = new ConsoleStudentPrinter();
        IAverageStrategy  strategy  = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter();

        var service     = new StudentService(repository, printer, strategy, validator);
        var solidDemo   = new SolidDemoService(service);

        IMenuService menu = new ConsoleMenuService(service, solidDemo);
        menu.Run();
    }
}
