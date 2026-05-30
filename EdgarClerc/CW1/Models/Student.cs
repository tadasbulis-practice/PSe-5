namespace CW1.Models;

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string GroupCode { get; }
    public List<float> Grades { get; } = new();

    public Student(int id, string name, string email, string groupCode, List<float> grades)
    {
        Id = id;
        Name = name;
        Email = email;
        GroupCode = groupCode;
        Grades = grades;
    }

    public override string ToString()
    {
        return $"[{Id}] {Name} ({GroupCode}) email={Email} grades=[{string.Join(',', Grades)}]";
    }
}
