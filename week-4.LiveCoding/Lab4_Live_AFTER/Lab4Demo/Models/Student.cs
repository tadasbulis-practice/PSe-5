namespace Lab4Demo.Models;

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }

    public Student(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
