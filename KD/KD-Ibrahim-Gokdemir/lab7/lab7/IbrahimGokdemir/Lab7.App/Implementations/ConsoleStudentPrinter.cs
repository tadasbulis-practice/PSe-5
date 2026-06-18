using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(IReadOnlyList<Student> students)
    {
        Console.WriteLine("Students:");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.Id} {s.Name} {s.Email} ({s.GroupCode ?? "no group"})");
        }
    }
}
