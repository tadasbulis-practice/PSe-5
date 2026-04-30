using Lab4Demo.Implementations;
using Lab4Demo.Implementations.strategies;
using Lab4Demo.Interfaces;
using Lab4Demo.Models;
using Lab4Demo.Services;

class Program
{
    static void Main()
    {
        IStudentRepository repo = new MemoryStudentRepository();
        IStudentPrinter printer = new ConsoleStudentPrinter();
        IAverageStrategy strategy = new SimpleAverageStrategy();

        var service = new StudentService(repo, printer, strategy);
        service.Run();
    }
}