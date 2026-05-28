public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public StudentService(
        IStudentRepository repository,
        IStudentPrinter printer,
        IStudentValidator validator,
        IAverageStrategy averageStrategy)
    {
        _repository = repository;
        _printer = printer;
        _validator = validator;
        _averageStrategy = averageStrategy;
    }

    public void AddStudent(Student student)
    {
        _repository.Add(student);
    }

    public bool RemoveStudent(int id)
    {
        return _repository.Remove(id);
    }

    public void PrintAllStudents()
    {
        var students = _repository.GetAll();
        var validStudents = _validator.ValidateAll(students);
        _printer.PrintStudents(validStudents);
    }

    public double CalculateGroupAverage()
    {
        var students = _repository.GetAll();
        var validStudents = _validator.ValidateAll(students);
        return _averageStrategy.Calculate(validStudents);
    }

    public void ShowGroupAverage()
    {
        Console.WriteLine($"Group average: {CalculateGroupAverage():F2}");
    }
}
