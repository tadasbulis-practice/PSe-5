using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Services;

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
        IStudentValidator validator
    )
    {
        _repository = repository;
        _printer = printer;
        _strategy = strategy;
        _validator = validator;
    }

    public void PrintStudents(IReadOnlyList<Student> students)
    {
        _printer.Print(students);
    }

    public Student AddStudent(Student student)
    {
        return _repository.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        _repository.Remove(student);
    }

    public Student? FindStudentById(int id)
    {
        return _repository.FindById(id);
    }

    public IReadOnlyList<Student> GetAllStudents()
    {
        return _repository.GetAll();
    }

    public void AddGrade(Student student, int grade)
    {
        _repository.AddGrade(student, grade);
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
