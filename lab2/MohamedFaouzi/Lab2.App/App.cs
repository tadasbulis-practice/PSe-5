using System.Collections.Generic;

public class App
{
    public void Run()
    {
        List<Student> students = new List<Student>
        {
            new Student("Ali", "Ait", new List<double> { 8, 9, 7 }),
            new Student("Sara", "Ben", new List<double> { 6, 7, 8 }),
            new Student("Omar", "El", new List<double> { 9, 9, 10 })
        };

        foreach (Student student in students)
        {
            student.PrintInfo();
        }
    }
}