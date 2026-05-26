using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Printer;

public class FileStudentPrinter : IStudentPrinter
{
    public void Print(IReadOnlyList<Student> students)
    {
        foreach (var student in students)
        {
            File.WriteAllText("student.txt", "...");
        }
    }
}
