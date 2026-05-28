using JohnBastille.Lab_5.Interfaces;
using JohnBastille.Lab_5.Models;

namespace Lab_5.Services;

public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"{student.Name} - ID: {student.Id}");
    }
}