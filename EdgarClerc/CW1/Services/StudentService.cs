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
}
