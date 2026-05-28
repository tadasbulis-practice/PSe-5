namespace lab3.model;

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

    public override string ToString()
    {
        var str = "Group name : " + _name + "\n Students :";
        foreach (var student in Students)
            str += "\n" + student;

        return str;
    }
}
