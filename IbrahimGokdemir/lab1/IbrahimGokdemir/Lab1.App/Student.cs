using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public List<int> Grades { get; }

    public Student(int id, string firstName, string lastName, string email, List<int> grades)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Grades = grades;
    }

    public double GetAverage()
    {
        return Grades.Average();
    }

    public string Describe()
    {
        return $"{Id} - {FirstName} {LastName} | Email: {Email}";
    }
}
