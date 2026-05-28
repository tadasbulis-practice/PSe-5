using System.Text.Json;

public class JsonStudentPrinter : IStudentPrinter
{
    public void Print(List<Student> students)
    {
        string json = JsonSerializer.Serialize(students, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("students.json", json);
        Console.WriteLine("Students saved to students.json");
    }
}