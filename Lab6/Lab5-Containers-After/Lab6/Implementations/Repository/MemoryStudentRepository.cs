using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Repository;

public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();

    public IReadOnlyList<Student> GetAll()
    {
        return _students.AsReadOnly();
    }

    public Student? FindById(int id)
    {
        return _students.FirstOrDefault(student => student.Id == id);
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }

    public bool Remove(int id)
    {
        var student = FindById(id);
        if (student is null)
        {
            return false;
        }

        return _students.Remove(student);
    }
}
