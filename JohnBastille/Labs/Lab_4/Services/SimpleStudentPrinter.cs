using JohnBastille.Lab_4.Interfaces;
using JohnBastille.Lab_4.Models;

namespace Lab_4.Services;

public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"{student.Name} - ID: {student.Id}");
    }
}