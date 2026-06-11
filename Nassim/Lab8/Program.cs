using Lab7.Implementations.Adapter;
using Lab7.Implementations.Menu;
using Lab7.Implementations.Printer;
using Lab7.Implementations.Repository;
using Lab7.Implementations.Strategy;
using Lab7.Interfaces;
using Lab7.Models;
using Lab7.Services;

namespace Lab7;

class Program
{
    static void Main(string[] args)
    {
        // ══════════════════════════════════════════════════════════════
        // TEST — Inheritance & Polymorphism (Lab-8)
        // ══════════════════════════════════════════════════════════════
        Console.WriteLine("=== Inheritance Test ===");

        Student s1 = new Student(1, "Alice", "Smith", "alice@uni.lt", "Computer Science", 2023);
        Student s2 = new GraduateStudent(99, "Ana", "Smith", "ana@uni.lt", "Computer Science", 2023, "SOLID in C#");

        Console.WriteLine(s1.Info()); // Student: Alice Smith
        Console.WriteLine(s2.Info()); // Student: Ana Smith | Thesis: SOLID in C#

        Console.WriteLine();

        // ══════════════════════════════════════════════════════════════
        // COMPOSITION ROOT
        // To switch behaviour:
        //   useApi     = true  → Docker API (docker compose up first)
        //   useLogging = true  → logs every repository call
        // ══════════════════════════════════════════════════════════════

        const string apiUrl     = "http://localhost:6001";
        const bool   useApi     = false;
        const bool   useLogging = false;

        IStudentRepository repository = useApi
            ? new ApiStudentRepository(apiUrl)
            : useLogging
                ? new LoggedStudentRepository()
                : new MemoryStudentRepository();

        IStudentPrinter   printer   = new ConsoleStudentPrinter();
        IAverageStrategy  strategy  = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter();

        var service = new StudentService(repository, printer, strategy, validator);

        IMenuService menu = new ConsoleMenuService(service);
        menu.Run();
    }
}