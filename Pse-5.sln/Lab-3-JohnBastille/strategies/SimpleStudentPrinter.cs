using Lab-3-JohnBastille.Interfaces;
using Lab-3-JohnBastille.Models;

namespace Lab-3-JohnBastille.Strategies;

public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student, double average)
    {
        Console.WriteLine($"{student.Id}: {student.Name} (avg: {average:F2})");
    }
}