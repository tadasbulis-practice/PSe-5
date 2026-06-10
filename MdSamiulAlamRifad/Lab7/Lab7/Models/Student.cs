namespace Lab7.Models;

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
        if (grade < 1 || grade > 10)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 1 and 10.");

        _grades.Add(grade);
    }

    public double GetAverage() =>
        _grades.Count == 0 ? 0 : _grades.Average();
}
