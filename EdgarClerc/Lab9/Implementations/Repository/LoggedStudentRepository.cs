using Lab9.Models;

namespace Lab9.Implementations.Repository;

public class LoggedStudentRepository : MemoryStudentRepository
{
    public LoggedStudentRepository()
        : base() { }

    public override IReadOnlyList<Student> GetAll()
    {
        Console.WriteLine($"[LOG] GetAll() → {StudentsById.Count} students");
        return base.GetAll();
    }

    public override void Add(Student student)
    {
        Console.WriteLine($"[LOG] Add({student.FullName})");
        base.Add(student);
    }

    public override bool Remove(int id)
    {
        Console.WriteLine($"[LOG] Remove(id={id})");
        return base.Remove(id);
    }
}
