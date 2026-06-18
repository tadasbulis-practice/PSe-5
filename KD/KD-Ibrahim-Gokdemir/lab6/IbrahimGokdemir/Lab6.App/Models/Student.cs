namespace Lab6.App.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<int> Grades { get; set; }

    public Student(int id, string name, string email, List<int> grades)
    {
        Id = id;
        Name = name;
        Email = email;
        Grades = grades;
    }
}