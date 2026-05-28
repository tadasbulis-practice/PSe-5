using System.Text.Json;
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        foreach (var student in group.Students)
        {
            Console.WriteLine(JsonSerializer.Serialize(student));
        }
    }
}
