using Week3.LiveCoding.Models;

namespace Week3.LiveCoding.Services;

public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}
public class SimpleStudentPrinter : IStudentPrinter
{
    public void Print(Student student)
    {
        Console.WriteLine($"{student.Name} ({student.Age})");
    }
}
