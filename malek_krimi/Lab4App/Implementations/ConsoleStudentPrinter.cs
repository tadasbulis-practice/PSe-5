public class ConsoleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"Name: {student.Name}, Grades: {string.Join(", ", student.Grades)}");
    }
}
