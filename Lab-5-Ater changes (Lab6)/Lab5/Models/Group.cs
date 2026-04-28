
namespace Lab5.Models;

public class Group
{
    private readonly List<Student> _students = new();
    public IReadOnlyList<Student> Students => _students;

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }
}
