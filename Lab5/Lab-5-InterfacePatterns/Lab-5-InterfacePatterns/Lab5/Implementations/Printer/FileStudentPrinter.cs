using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

public class FileStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        List<string> lines = new List<string>();

        foreach (var student in group.Students)
        {
            lines.Add($"{student.Id} {student.Name} {student.Email}");
        }

        File.WriteAllLines("students.txt", lines);
        Console.WriteLine("Students were written to students.txt");
    }
}