using System.Text.Json;
using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Printer;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(IReadOnlyList<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine(JsonSerializer.Serialize(student));
        }
    }
}
