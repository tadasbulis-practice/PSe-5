namespace lab3.Edgar.model;

public class Group
{
    private readonly string _name;

    public Group(string name)
    {
        _name = name;
        Students = [];
    }

    public List<StudentProfile> Students { get; }

    public void AddStudent(StudentProfile student)
    {
        Students.Add(student);
    }

    public void PrintToString()
    {
        Console.WriteLine("Group :  " + _name);
        Console.WriteLine("Students :");
        foreach (var student in Students)
            Console.WriteLine(student.ToString());
    }
}
