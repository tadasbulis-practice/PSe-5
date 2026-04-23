using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;

namespace Lab_3.Services;

public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"{student.Name} - ID: {student.Id}");
    }
}