namespace CW1After.Models;

public class Student
{
    public int    Id        { get; set; }
    public string Name      { get; set; } = "";
    public string Email     { get; set; } = "";
    public string GroupCode { get; set; } = "";
    public List<int> Grades { get; set; } = new();
}
