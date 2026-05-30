using CW1.Interface;
using CW1.Models;

namespace CW1.Repository;

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public StubStudentRepository()
    {
        // Initialize with ONLY the one required Group
        _groups = new List<Group> { new Group(code: "TEST", name: "Test group") };

        // Initialize with ONLY the one required Student
        _students = new List<Student>
        {
            new Student(
                id: 999,
                name: "Test Student",
                email: "test@test.lt",
                groupCode: "TEST",
                grades: new List<float> { 10, 10, 10 }
            ),
        };
    }

    public IReadOnlyList<Student> FindAll() => _students.AsReadOnly();

    public IReadOnlyList<Group> FindAllGroups() => _groups.AsReadOnly();

    public Student FindById(int id)
    {
        var student = _students.Find(s => s.Id == id);
        return student ?? throw new Exception("Student not found");
    }

    public Group FindGroupByCode(string code)
    {
        var group = _groups.Find(g => g.Code == code);
        return group ?? throw new Exception("Group not found");
    }

    public void Add(Student student)
    {
        Console.WriteLine("Can't add student when stubRepository enabled");
        throw new NotSupportedException(
            "Adding students is not supported in StubStudentRepository."
        );
    }

    public void Remove(Student student)
    {
        Console.WriteLine("Can't remove student when stubRepository enabled");
        throw new NotSupportedException(
            "Removing students is not supported in StubStudentRepository."
        );
    }
}
