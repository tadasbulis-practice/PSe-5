
using Week3.LiveCoding.Advanced.Models;
using Week3.LiveCoding.Advanced.Strategies;

namespace Week3.LiveCoding.Advanced.Services;

public class StudentService : IStudentService
{
    private readonly IGradeStrategy _strategy;

    public StudentService(IGradeStrategy strategy)
    {
        _strategy = strategy;
    }

    public void AddStudent(Group group, Student student)
    {
        group.Students.Add(student);
    }

    public void PrintAll(Group group)
    {
        Console.WriteLine($"Students in group: {group.Name}");

        foreach (var s in group.Students)
        {
            double result = _strategy.Calculate(s);
            Console.WriteLine($"{s.Name} -> Result: {result}");
        }
    }
}
