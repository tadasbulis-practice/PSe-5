using StudentApi.Interfaces;
using StudentApi.Models;

namespace StudentApi.Repositories;

public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student) => _students.Add(student);
}

