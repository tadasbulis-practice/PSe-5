namespace Lab_3_JohnBastille.Models-3-JohnBastille;

public class Group
{
    public string Name { get; }
    private readonly List<Student> _students = new();

    public Group(string name)
    {
        Name = name;
    }

    public IReadOnlyCollection<Student> Students => _students;

    public void AddStudent(Student student) => _students.Add(student);
}