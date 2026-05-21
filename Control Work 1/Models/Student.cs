namespace CW1Friend.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string GroupCode { get; set; }
    public List<int> Grades { get; set; }

    public Student()
    {
        FullName = string.Empty;
        EmailAddress = string.Empty;
        GroupCode = string.Empty;
        Grades = new List<int>();
    }
}
