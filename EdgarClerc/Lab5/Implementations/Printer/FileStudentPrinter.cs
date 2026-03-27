using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

public class FileStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        foreach (var student in group.Students)
        {
            File.WriteAllText("student.txt", "...");
        }
    }
}
