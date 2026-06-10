using Lab5.Interfaces;
using Lab5.Models;
using System.Text.Json;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        string json = JsonSerializer.Serialize(group.Students);
        File.WriteAllText("students.json", json);
    }
}
