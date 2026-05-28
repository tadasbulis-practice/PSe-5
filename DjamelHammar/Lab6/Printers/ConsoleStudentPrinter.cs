public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        Console.WriteLine("=== STUDENTS ===");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.Id} - {s.Name} -> {string.Join(", ", s.Grades)}");
        }
    }
}
