using Lab5.Interfaces;
using Lab5.Services;
using Lab5.Implementations.Repository;
using Lab5.Implementations.Printer;
using Lab5.Implementations.Strategy;
using Lab5.Implementations.Menu;
using Lab5.Implementations.Adapter;

namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        IStudentRepository repository = new MemoryStudentRepository();

        IStudentPrinter printer = new JsonStudentPrinter();

        IAverageStrategy strategy = new MedianAverageStrategy();

        IStudentValidator validator = new StudentValidatorAdapter();

        StudentService service = new StudentService(repository, printer, strategy, validator);

        IMenuService menu = new ConsoleMenuService(service);
        menu.Run();
    }
}