using lab3.model;
using Lab3.Service;

namespace lab3;

internal class Program
{
    private static void Main()
    {
        var studentPrinter = new StudentPrinter();
        var averageStrategy = new AverageStrategy();

        var student1 = new StudentProfile(
            "edgar",
            "clerc",
            "erasmus",
            new DateOnly(2026, 02, 19),
            [10, 12, 16, 14.5, 18.2]
        );

        var student2 = new StudentProfile(
            "nassim",
            "???",
            "erasmus",
            new DateOnly(2026, 02, 12),
            [2, 3, 5, 2.3]
        );

        var student3 = new StudentProfile(
            "ibrahim",
            "???",
            "erasmus",
            new DateOnly(2026, 02, 12),
            [18.2, 19, 17, 16.8, 18]
        );

        averageStrategy.Average(student1);

        var group = new Group("group1");

        group.AddStudent(student1);
        group.AddStudent(student2);
        group.AddStudent(student3);

        Console.WriteLine(studentPrinter.PrintGroup(group));
    }
}
