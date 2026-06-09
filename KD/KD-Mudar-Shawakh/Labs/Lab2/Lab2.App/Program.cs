using Lab2.App.Models;
using Lab2.App.Services;

namespace Lab2.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new StudentService();

            var s1 = new Student("Student 1");
            service.AddGrade(s1, 75);
            service.AddGrade(s1, 80);
            service.AddGrade(s1, 90);
            service.AddGrade(s1, 65);

            var s2 = new Student("Student 2");
            service.AddGrade(s2, 100);
            service.AddGrade(s2, 92);
            service.AddGrade(s2, 73);
            service.AddGrade(s2, 86);

            Console.WriteLine(s1.Describe());
            Console.WriteLine(s2.Describe());

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
