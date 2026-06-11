using System;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    internal class Program
    {
        private static void Main()
        {
            var student1 = new Student(1, "Nassim", "Barad", "nassim@email.com", 14.1);
            var student2 = new Student(2, "Edgar", "Clerc", "edgar@email.com", 3.1);
            var student3 = new Student(3, "Ibrahim", "???", "ibrahim@email.com", 17.8);

            var group = new Group("Erasmus");
            group.AddStudent(student1);
            group.AddStudent(student2);
            group.AddStudent(student3);

            // Strategy 1 : Console
            Console.WriteLine("--- Strategy 1: ConsolePrinter ---");
            IStudentPrinter console = new ConsolePrinter();
            new GroupService(console).DisplayGroup(group);

            // Strategy 2 : Short
            Console.WriteLine("\n--- Strategy 2: ShortPrinter ---");
            IStudentPrinter shortP = new ShortPrinter();
            new GroupService(shortP).DisplayGroup(group);

            // Strategy 3 : File
            Console.WriteLine("\n--- Strategy 3: FileStudentPrinter ---");
            IStudentPrinter file = new FileStudentPrinter("students.txt");
            new GroupService(file).DisplayGroup(group);
            Console.WriteLine("Output written to students.txt");

            // Strategy 4 : JSON
            Console.WriteLine("\n--- Strategy 4: JsonStudentPrinter ---");
            IStudentPrinter json = new JsonStudentPrinter();
            new GroupService(json).DisplayGroup(group);

            // Runtime switching demo
            Console.WriteLine("\n--- Runtime Switching Demo ---");
            IStudentPrinter[] strategies = {
                new ConsolePrinter(),
                new ShortPrinter(),
                new JsonStudentPrinter()
            };

            foreach (var strategy in strategies)
            {
                Console.WriteLine($"\n[Using: {strategy.GetType().Name}]");
                new GroupService(strategy).DisplayGroup(group);
            }
        }
    }
}