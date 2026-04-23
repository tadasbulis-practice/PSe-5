public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        Console.WriteLine("=== STUDENTS ===");
        students.ForEach(s =>
            Console.WriteLine($"{s.Id} - {s.Name} -> {string.Join(", ", s.Grades)}"));
    }
}
