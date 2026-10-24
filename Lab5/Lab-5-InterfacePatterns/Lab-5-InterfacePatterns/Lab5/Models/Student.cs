
namespace Lab5.Models;

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }

    private readonly List<int> _grades = new();
    public IReadOnlyList<int> Grades => _grades;

    public Student(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public void AddGrade(int grade)
    {
        _grades.Add(grade);
    }
}
