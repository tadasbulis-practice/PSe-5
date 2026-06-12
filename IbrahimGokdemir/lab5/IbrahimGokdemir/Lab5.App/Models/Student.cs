using System.Collections.Generic;

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public List<int> Grades { get; }

    public Student(int id, string name, string email, List<int> grades)
    {
        Id = id;
        Name = name;
        Email = email;
        Grades = grades;
    }

    public string Describe()
    {
        return $"{Id} {Name} {Email}";
    }
}