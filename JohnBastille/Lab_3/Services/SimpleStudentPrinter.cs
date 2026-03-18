using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;
public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"{student.Name} - ID: {student.Id}");
    }
}