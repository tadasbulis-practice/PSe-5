
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

using System.Collections.Generic;
using System.Linq;

public class MemoryStudentRepository : IStudentRepository
{
    private List<Student> students = new();

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
