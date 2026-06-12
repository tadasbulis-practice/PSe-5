
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IAverageStrategy _strategy;
    private readonly IStudentValidator _validator;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IAverageStrategy strategy,
        IStudentValidator validator)
    {
        _repository = repository;
        _printer = printer;
        _strategy = strategy;
        _validator = validator;
    }

    public void PrintGroup(Group group)
    {
        _printer.Print(group);
    }

    public double CalculateAverage(Student student)
    {
        return _strategy.Calculate(student);
    }

    public bool ValidateStudent(Student student)
    {
        return _validator.Validate(student);
    }

    public Student? FindStudent(string query)
    {
        return _repository.Find(query);
    }
}
