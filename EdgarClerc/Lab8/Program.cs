using Lab8.Implementations.Adapter;
using Lab8.Implementations.Menu;
using Lab8.Implementations.Printer;
using Lab8.Implementations.Repository;
using Lab8.Implementations.Strategy;
using Lab8.Interfaces;
using Lab8.Models;
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
