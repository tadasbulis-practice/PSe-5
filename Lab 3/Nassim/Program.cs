using lab3.Edgar.model;
using Lab3.Edgar.Service;

namespace lab3.Nassim;

internal class Program
{
    private static void Main()
    {
        var studentPrinter = new StudentPrinter();
        var averageStrategy = new AverageStrategy();

        var student1 = new StudentProfile(
            "nassim",
            "barad",
            "erasmus",
            new DateOnly(2026, 02, 26),
            [10, 12, 16, 14.5, 18.2]
        );

        var student2 = new StudentProfile(
            "edgar",
            "Clerc",
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

 
        var group = new Group("group1");

        group.AddStudent(student1);
        group.AddStudent(student2);
        group.AddStudent(student3);

        studentPrinter.PrintGroup(group);
    }
}
