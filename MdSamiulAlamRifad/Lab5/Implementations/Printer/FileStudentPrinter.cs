using Lab5.Interfaces;
using Lab5.Models;

public class FileStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        File.WriteAllText("students.txt",
            string.Join("\n", group.Students.Select(s => $"{s.Name} - {s.Email}")));
    }
}
