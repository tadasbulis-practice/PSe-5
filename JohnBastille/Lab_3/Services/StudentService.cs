namespace JohnBastille.Lab_3.Services;

using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;


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
            
        }
    }
}