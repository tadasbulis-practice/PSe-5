
namespace Lab5.Models;

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public double Grade { get; }
    public double Weight { get; } // used by WeightedAverageStrategy

    public Student(int id, string name, double grade, double weight = 1)
    {
        Id = id;
        Name = name;
        Grade = grade;
        Weight = weight;
    }
}
