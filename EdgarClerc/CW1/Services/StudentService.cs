using CW1.Interface;
using CW1.Models;

namespace CW1.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<Student> getAll()
    {
        return _repository.FindAll();
    }

    public void Add(Student student)
    {
        _repository.Add(student);
    }

    public void Remove(Student student)
    {
        _repository.Remove(student);
    }

    public Student GetById(int id)
    {
        return _repository.FindById(id);
    }
}
