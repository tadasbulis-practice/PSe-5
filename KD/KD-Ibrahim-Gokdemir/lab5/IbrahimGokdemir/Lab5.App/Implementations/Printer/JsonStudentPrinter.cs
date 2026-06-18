using System;
using System.IO;
using System.Text.Json;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        string json = JsonSerializer.Serialize(group.Students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("students.json", json);
        Console.WriteLine("Students written to students.json");
    }
}