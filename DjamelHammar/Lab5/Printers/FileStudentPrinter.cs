public class FileStudentPrinter : IStudentPrinter
{
    public void Print(List<Student> students)
    {
        var lines = students.Select(s => $"{s.Name} -> {string.Join(", ", s.Grades)}");
        File.WriteAllLines("students.txt", lines);
        Console.WriteLine("Students saved to students.txt");
    }
}