namespace CW1.Models;

/// <summary>
/// Represents a student with basic info and a list of grades.
/// </summary>
public class Student
{
    public int Id;
    public string Name = "";
    public string Email = "";
    public string GroupCode = "";
    public List<int> Grades = new();
}
