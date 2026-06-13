using Lab3.Models;

public class FancyStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        Console.WriteLine($"=== Students in {group.Name} ===");

        foreach (var s in group.Students)
        {
            Console.WriteLine($"* [{s.Id}] {s.Name}");
        }

        Console.WriteLine("===============================");
    }
}
