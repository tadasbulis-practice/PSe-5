using Lab7.App.Interfaces;

namespace Lab7.App.Models;

public class Student : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<int> Grades { get; set; }

    // New in Lab 7: links a Student to a Group, so we can group/report by it.
    public string? GroupCode { get; set; }

    public Student(int id, string name, string email, List<int> grades, string? groupCode = null)
    {
        Id = id;
        Name = name;
        Email = email;
        Grades = grades;
        GroupCode = groupCode;
    }
}
