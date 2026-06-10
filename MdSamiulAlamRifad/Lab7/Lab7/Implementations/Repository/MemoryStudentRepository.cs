using Lab7.Interfaces;
using Lab7.Models;

namespace Lab7.Implementations.Repository;

// Task 1 — Repository with Container
// Container is PRIVATE — access only through methods (encapsulation rule)
public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();

    public List<Student> GetAll()
    {
        // Return a copy so callers cannot mutate the internal list directly
        return new List<Student>(_students);
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }

    public bool Remove(int id)
    {
        var student = GetById(id);
        if (student is null) return false;
        return _students.Remove(student);
    }
}
