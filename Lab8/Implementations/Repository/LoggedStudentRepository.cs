using Lab7.Models;

namespace Lab7.Implementations.Repository;

public class LoggedStudentRepository : MemoryStudentRepository
{
    public override IReadOnlyList<Student> GetAll()
    {
        Console.WriteLine($"[LOG] GetAll() called");
        return base.GetAll();
    }

    public override void Add(Student student)
    {
        Console.WriteLine($"[LOG] Add() called for {student.FullName}");
        base.Add(student);
    }

    public override bool Remove(int id)
    {
        Console.WriteLine($"[LOG] Remove() called for id={id}");
        return base.Remove(id);
    }
}