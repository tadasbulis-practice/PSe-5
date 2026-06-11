using System;

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

            // Scenario 1 : impression réelle
            Console.WriteLine("--- Scenario 1: ConsolePrinter ---");
            IStudentPrinter real = new ConsolePrinter();
            var service1 = new GroupService(real);
            service1.DisplayGroup(group);

            // Scenario 2 : Fake (résultat stable)
            Console.WriteLine("\n--- Scenario 2: FakePrinter ---");
            IStudentPrinter fake = new FakePrinter();
            var service2 = new GroupService(fake);
            service2.DisplayGroup(group);

            // Scenario 3 : Stub avec notes visibles
            Console.WriteLine("\n--- Scenario 3: StubPrinter (grades ON) ---");
            IStudentPrinter stubOn = new StubPrinter(showGrades: true);
            var service3 = new GroupService(stubOn);
            service3.DisplayGroup(group);

            // Scenario 4 : Stub sans notes
            Console.WriteLine("\n--- Scenario 4: StubPrinter (grades OFF) ---");
            IStudentPrinter stubOff = new StubPrinter(showGrades: false);
            var service4 = new GroupService(stubOff);
            service4.DisplayGroup(group);
        }
    }
}