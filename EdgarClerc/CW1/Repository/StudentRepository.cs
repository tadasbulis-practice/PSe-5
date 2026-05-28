using CW1.Interface;
using CW1.Models;

namespace CW1.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();
    private readonly List<Group> _groups = new();

    public StudentRepository()
    {
        //Init demo student data
        _students = new List<Student>([
            new Student(
                id: 1,
                name: "Jonas Jonaitis",
                email: "jonas@kauko.lt",
                groupCode: "PI23",
                grades: new List<int> { 8, 9, 7, 10 }
            ),
            new Student(
                id: 2,
                name: "Greta Petraityte",
                email: "greta@kauko.lt",
                groupCode: "PI23",
                grades: new List<int> { 6, 5, 7, 8 }
            ),
            new Student(
                id: 3,
                name: "Mantas Kazlauskas",
                email: "mantas@kauko.lt",
                groupCode: "PI24",
                grades: new List<int> { 9, 9, 10, 8 }
            ),
            new Student(
                id: 4,
                name: "Ieva Andriukaityte",
                email: "ieva@kauko.lt",
                groupCode: "PI23",
                grades: new List<int> { 10, 10, 9, 9 }
            ),
            new Student(
                id: 5,
                name: "Tomas Bagdonas",
                email: "tomas@kauko.lt",
                groupCode: "PI24",
                grades: new List<int> { 5, 6, 6, 7 }
            ),
        ]);

        //Init demo group data
        _groups = new List<Group>([
            new Group(code: "PI23", name: "Programu inzinerija 2023"),
            new Group(code: "PI24", name: "Programu inzinerija 2024"),
        ]);
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
        _students.Add(student);
    }

    public void Remove(Student student)
    {
        _students.Remove(student);
    }
}
