using Lab9.Implementations.Adapter;
using Lab9.Implementations.Menu;
using Lab9.Implementations.Printer;
using Lab9.Implementations.Repository;
using Lab9.Implementations.Strategy;
using Lab9.Interfaces;
using Lab9.Models;
using Lab9.Services;

namespace Lab9;

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
        const bool useApi = false; // ← set true when Docker is running

        const bool useLogging = true;
        IStudentRepository repository = useLogging
            ? new LoggedStudentRepository()
            : new MemoryStudentRepository();

        IStudentPrinter printer = new ConsoleStudentPrinter();
        IAverageStrategy strategy = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter();

        var service = new StudentService(repository, printer, strategy, validator);

        var g = new GraduateStudent(99, "Ana", "Smith", "a@uni.lt", "CS", 2023, "SOLID in C#");
        Console.WriteLine(g.Info());

        IMenuService menu = new ConsoleMenuService(service);
        menu.Run();
    }
}
