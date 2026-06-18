using Lab6.App.Interfaces;

namespace Lab6.App.Services;

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

    public void Run()
    {
        var students = _repository.GetAll();
        var validStudents = _validator.ValidateAll(students);

        _printer.PrintStudents(validStudents);

        var avg = _strategy.Calculate(validStudents);
        Console.WriteLine($"\nGroup Average: {avg:0.00}");
    }
}