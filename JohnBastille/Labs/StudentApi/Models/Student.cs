namespace StudentApi.Models;

public class Student
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public List<int> Grades { get; }

    public Student(string name, int age, List<int> grades)
    {
        Id = _nextId++;
        Name = name;
        Age = age;
        Grades = grades;
    }
}
