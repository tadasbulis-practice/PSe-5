using System;
using Lab6.App.Interfaces;
using Lab6.App.Services;

namespace Lab6.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==============================================================
            // Composition Root — the ONLY place where concrete classes are
            // chosen. Everything below this method depends on interfaces only.
            // To swap a printer / strategy / repository: change ONE line here.
            // ==============================================================

            // --- Choose implementations ---
            IStudentRepository repository = new MemoryStudentRepository();
            IStudentPrinter printer = AskPrinter();
            IAverageStrategy strategy = AskStrategy();
            IStudentValidator validator = new StudentValidator();

            // --- Wire them into the service ---
            var service = new StudentService(repository, printer, strategy, validator);

            // --- Drive the demo through the menu ---
            IMenuService menu = new ConsoleMenuService(service);
            menu.Run();
        }

        // ---------- Runtime switching (LAB-5 carried into LAB-6) ----------

        private static IStudentPrinter AskPrinter()
        {
            Console.WriteLine("Select printer:");
            Console.WriteLine("1. Console (default)");
            Console.WriteLine("2. File   (students.txt)");
            Console.WriteLine("3. JSON   (students.json)");
            Console.Write("Choice [1]: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "2": return new FileStudentPrinter();
                case "3": return new JsonStudentPrinter();
                default:  return new ConsoleStudentPrinter();
            }
        }

        private static IAverageStrategy AskStrategy()
        {
            Console.WriteLine("\nSelect average strategy:");
            Console.WriteLine("1. Simple   (default)");
            Console.WriteLine("2. Weighted");
            Console.WriteLine("3. Median");
            Console.Write("Choice [1]: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "2": return new WeightedAverageStrategy();
                case "3": return new MedianAverageStrategy();
                default:  return new SimpleAverageStrategy();
            }
        }
    }
}