using Lab7.Implementations.Adapter;
using Lab7.Implementations.Menu;
using Lab7.Implementations.Printer;
using Lab7.Implementations.Repository;
using Lab7.Implementations.Strategy;
using Lab7.Interfaces;
using Lab7.Services;

namespace Lab7;

class Program
{
    static void Main(string[] args)
    {
        // ══════════════════════════════════════════════════════════════
        // COMPOSITION ROOT — the only place where concrete classes are
        // chosen. Everything else uses interfaces.
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

        var service = new StudentService(repository, printer, strategy, validator);

        IMenuService menu = new ConsoleMenuService(service);
        menu.Run();
    }
}
