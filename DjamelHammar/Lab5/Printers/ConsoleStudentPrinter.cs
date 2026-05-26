public class ConsoleStudentPrinter : IStudentPrinter
{
    public void Print(List<Student> students)
    {
        Console.WriteLine("Students in console:");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} -> {string.Join(", ", s.Grades)}");
        }
    }
}