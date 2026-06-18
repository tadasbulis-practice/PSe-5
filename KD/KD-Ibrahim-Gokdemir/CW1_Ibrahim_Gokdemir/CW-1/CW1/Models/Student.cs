using System.Collections.Generic;

namespace CW1.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string GroupCode { get; set; } = string.Empty;
    public List<int> Grades { get; set; } = new();
}