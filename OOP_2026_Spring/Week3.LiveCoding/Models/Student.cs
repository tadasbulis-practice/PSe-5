namespace Week3.LiveCoding.Models;

public class Student
{
    public int Id { get; }
    public string Name { get; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Describe()
    {
        return $"ID: {Id}, Name: {Name}";
    }
}
