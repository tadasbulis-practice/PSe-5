using Week3.LiveCoding.Models;

namespace Week3.LiveCoding.Services;

public class StudentService : IStudentService
{
    public void AddStudent(Group group, Student student)
    {
        group.Students.Add(student);
        Console.WriteLine("Student added.");
    }

    public void PrintAll(Group group)
    {
        Console.WriteLine($"Students in group: {group.Name}");

        foreach (var s in group.Students)
        {
            Console.WriteLine(s.Describe());
        }
    }
}
