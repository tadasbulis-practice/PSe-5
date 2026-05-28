using System;

class Program
{
    static void Main(string[] args)
    {
        
        Group group = new Group();
        group.Name = "CS-1";

        
        Student s1 = new Student
        {
            Id = 1,
            FirstName = "md",
            LastName = "faouzi",
            Email = "Md@email.com",
            AverageGrade = 85.5
        };

        Student s2 = new Student
        {
            Id = 2,
            FirstName = "malek",
            LastName = "mibon",
            Email = "malek@email.com",
            AverageGrade = 90.2
        };

        Student s3 = new Student
        {
            Id = 3,
            FirstName = "John",
            LastName = "bastile",
            Email = "john@email.com",
            AverageGrade = 78.4
        };

        
        group.AddStudent(s1);
        group.AddStudent(s2);
        group.AddStudent(s3);

        
        group.PrintAll();
    }
}
