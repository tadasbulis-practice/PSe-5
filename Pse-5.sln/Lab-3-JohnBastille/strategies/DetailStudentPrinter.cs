using Lab3.StrategyDemo.Interfaces;
using Lab3.StrategyDemo.Models;

namespace Lab3.StrategyDemo.Strategies;

public class DetailedStudentPrinter : IStudentPrinter
{
    public void Print(Student student, double average)
    {
        Console.WriteLine("----- Student -----");
        Console.WriteLine($"Id: {student.Id}");
        Console.WriteLine($"Name: {student.Name}");
        Console.WriteLine($"Grades: {string.Join(", ", student.Grades)}");
        Console.WriteLine($"Average: {average:F2}");
        Console.WriteLine("-------------------");
    }
}