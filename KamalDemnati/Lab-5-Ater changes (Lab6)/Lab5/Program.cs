
using Lab5.Implementations.Adapter;
using Lab5.Implementations.Menu;
using Lab5.Implementations.Printer;
using Lab5.Implementations.Repository;
using Lab5.Implementations.Strategy;
using Lab5.Interfaces;
using Lab5.Services;

namespace Lab5;

using Lab5.Legacy;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Dependency setup
        IStudentRepository repository = new MemoryStudentRepository();
        IStudentPrinter printer = new ConsoleStudentPrinter();
        IAverageStrategy strategy = new SimpleAverageStrategy();
        IStudentValidator validator = new StudentValidator();

        StudentService service = new StudentService(
            repository,
            printer,
            strategy,
            validator
        );

        // Menu
        IMenuService menu = new ConsoleMenuService(service);

        // Start app
        menu.Run();
    }
}