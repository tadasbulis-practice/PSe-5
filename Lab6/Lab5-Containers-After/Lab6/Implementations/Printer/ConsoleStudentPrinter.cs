using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Printer;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(IReadOnlyList<Student> students)
    {
        Console.WriteLine("--- Students ---");

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id}: {student.Name} | {student.Email} | Grades: {string.Join(", ", student.Grades)}");
        }
    }
}
