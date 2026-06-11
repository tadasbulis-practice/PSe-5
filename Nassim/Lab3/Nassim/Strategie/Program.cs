using System;

namespace Nassim.Lab3.Nassim.Service
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

            Console.WriteLine("--- Console Printer ---");
            IStudentPrinter printer1 = new ConsolePrinter();
            printer1.Print(group);

            Console.WriteLine("\n--- Short Printer ---");
            IStudentPrinter printer2 = new ShortPrinter();
            printer2.Print(group);
        }
    }
}