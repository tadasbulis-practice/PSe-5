public class StudentService
{
    private readonly IRepository<Student> _repository;
    private readonly IStudentPrinter _printer;
    private readonly IStudentValidator _validator;
    private readonly IAverageStrategy _averageStrategy;

    public StudentService(
        IRepository<Student> repository,
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

    public Student? GetStudentById(int id)
    {
        return _repository.GetById(id);
    }

    public List<Student> GetAllStudents()
    {
        return _repository.GetAll();
    }

    public List<Student> GetValidStudents()
    {
        return _validator.ValidateAll(_repository.GetAll());
    }

    public void PrintAllStudents()
    {
        _printer.PrintStudents(GetValidStudents());
    }

    public double CalculateGroupAverage()
    {
        return _averageStrategy.Calculate(GetValidStudents());
    }
}
