namespace ConsoleApp1.Edgar.model;

public class Group
{
    public Group(string name)
    {
        Name = name;
        Students = [];
    }

    private string Name { get; }
    private List<StudentProfile> Students { get; }

    public void AddStudent(StudentProfile student)
    {
        Students.Add(student);
    }

    public void PrintToString()
    {
        Console.WriteLine("Group :  " + Name);
        Console.WriteLine("Students :");
        foreach (var student in Students)
            Console.WriteLine(student.ToString());
    }
}
