using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentService
{
    private readonly IStudentRepository repository;

    private readonly StudentValidator validator;

    public StudentService(
        IStudentRepository repository,
        StudentValidator validator)
    {
        this.repository = repository;
        this.validator = validator;
    }

    public List<Student> GetAllStudents()
    {
        return repository.GetStudents();
    }

    public List<Group> GetAllGroups()
    {
        return repository.GetGroups();
    }

    public void AddStudent(Student student)
    {
        validator.Validate(student);

        repository.Add(student);
    }
}