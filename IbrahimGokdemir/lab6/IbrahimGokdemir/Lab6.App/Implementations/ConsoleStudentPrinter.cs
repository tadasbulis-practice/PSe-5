using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Implementations;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        Console.WriteLine("Students:");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.Id} {s.Name} {s.Email}");
        }
    }
}