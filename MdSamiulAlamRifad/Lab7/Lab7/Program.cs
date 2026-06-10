using Lab7.Implementations.Menu;
using Lab7.Implementations.Printer;
using Lab7.Implementations.Repository;
using Lab7.Implementations.Strategy;
using Lab7.Implementations.Validator;
using Lab7.Interfaces;
using Lab7.Services;

namespace Lab7;

// Composition Root — the ONLY place where concrete classes are instantiated.
// Program.cs has zero business logic. Everything wired via interfaces.
class Program
{
    static void Main(string[] args)
    {
        IStudentRepository repository = new MemoryStudentRepository();
        IStudentPrinter    printer    = new ConsoleStudentPrinter();
        IAverageStrategy   strategy   = new SimpleAverageStrategy();
        IStudentValidator  validator  = new StudentValidatorAdapter();

        var service   = new StudentService(repository, printer, strategy, validator);
        IMenuService menu = new ConsoleMenuService(service);

        menu.Run();
    }
}
