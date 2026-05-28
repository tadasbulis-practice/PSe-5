using JohnBastille.Lab_4.Interfaces;
using JohnBastille.Lab_4.Models;

namespace Lab_4.Services;

public class StudentService : IStudentService
{
    public void AddStudent(Group group, Student student)
    {
        group.Students.Add(student);
    }

    public void PrintAll(Group group)
    {
        Console.WriteLine($"Students in group: {group.Name}");

        foreach (var s in group.Students)
        {
            // Implementation can be added here
        }
    }
}