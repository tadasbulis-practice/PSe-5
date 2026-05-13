using Lab6.Implementations.Adapter;
using Lab6.Implementations.Menu;
using Lab6.Implementations.Printer;
using Lab6.Implementations.Repository;
using Lab6.Implementations.Strategy;
using Lab6.Interfaces;
using Lab6.Services;

namespace Lab6;

class Program
{
    static void Main(string[] args)
    {
        // Composition Root – only place where concrete classes are created.

        IStudentRepository repository = new MemoryStudentRepository();
        IStudentPrinter printer = new ConsoleStudentPrinter();
        IAverageStrategy strategy = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter();

        var service = new StudentService(repository, printer, strategy, validator);
        IMenuService menu = new ConsoleMenuService(service);

        menu.Run();
    }
}
