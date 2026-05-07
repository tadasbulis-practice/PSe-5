using System.Text.Json;
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        string json = JsonSerializer.Serialize(group.Students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("students.json", json);
        Console.WriteLine("Students were written to students.json");
    }
}