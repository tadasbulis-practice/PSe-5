using System;

namespace Nassim.Lab4.Nassim.Service
{
    internal class Program
    {
        private static void Main()
        {
            // Repository
            IStudentRepository repo = new MemoryStudentRepository();

            // Ajouter des étudiants
            repo.Add(new Student(1, "Nassim", "Barad", "nassim@email.com", 14.1));
            repo.Add(new Student(2, "Edgar", "Clerc", "edgar@email.com", 3.1));
            repo.Add(new Student(3, "Ibrahim", "???", "ibrahim@email.com", 17.8));

            // Injection des dépendances
            IStudentPrinter printer = new ConsolePrinter();
            IAverageStrategy strategy = new SimpleAverageStrategy();
            IStudentValidator validator = new StrictValidator();

            var service = new StudentService(repo, printer, strategy, validator);

            // Flow complet
            Console.WriteLine("=== All Students ===");
            service.PrintAllStudents();

            Console.WriteLine("\n=== Valid Students Only ===");
            service.PrintValidStudents();

            Console.WriteLine("\n=== Group Average (Simple) ===");
            Console.WriteLine($"Average: {service.CalculateGroupAverage():F2}");

            // Runtime switching — autre stratégie
            Console.WriteLine("\n=== Group Average (Median) ===");
            var service2 = new StudentService(repo, new ShortPrinter(), new MedianAverageStrategy(), new LenientValidator());
            Console.WriteLine($"Median Average: {service2.CalculateGroupAverage():F2}");
            service2.PrintAllStudents();
        }
    }
}