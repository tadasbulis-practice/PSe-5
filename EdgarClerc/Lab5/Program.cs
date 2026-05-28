using Lab5.Implementations.Adapter;
using Lab5.Implementations.Menu;
using Lab5.Implementations.Printer;
using Lab5.Implementations.Repository;
using Lab5.Implementations.Strategy;
using Lab5.Interfaces;
using Lab5.Services;

namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        // Composition Root – only place where concrete classes are created

        IStudentRepository repository = new MemoryStudentRepository();
        IStudentPrinter printer = new JsonStudentPrinter();
        IAverageStrategy strategy = new MedianAverageStrategy();
        IStudentValidator validator = new StudentValidatorAdapter();

        StudentService service = new StudentService(repository, printer, strategy, validator);

        IMenuService menu = new ConsoleMenuService(service);
        menu.Run();
    }
}
