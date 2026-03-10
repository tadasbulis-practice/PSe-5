using System;
namespace Lab_3_JohnBastille.Models;

	

public class Student
{
    public int Id { get; }
    public string Name { get; }
    public List<int> Grades { get; }

    public Student(int id, string name, List<int> grades)
    {
        Id = id;
        Name = name;
        Grades = grades;
    }
}
