using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Implementations;

public class MemoryStudentRepository : IStudentRepository
{
    private List<Student> students = new(); // 🔥 container burada

    public List<Student> GetAll()
    {
        return students;
    }

    public Student GetById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public void Add(Student student)
    {
        students.Add(student);
    }

    public bool Remove(int id)
    {
        var student = GetById(id);
        if (student == null) return false;
        students.Remove(student);
        return true;
    }
}