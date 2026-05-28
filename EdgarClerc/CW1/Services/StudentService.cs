using CW1.Interface;
using CW1.Models;
using CW1.Repository;

namespace CW1.Services;

public class StudentService
{
    private readonly IStudentRepository _repository;
    private readonly AverageStategy _averageStategy;
    private readonly StudentValidator _validator;
    private readonly IReportService _report;

    public StudentService(
        IStudentRepository repository,
        AverageStategy averageStategy,
        StudentValidator validator,
        IReportService report
    )
    {
        _repository = repository;
        _averageStategy = averageStategy;
        _validator = validator;
        _report = report;
    }

    public IReadOnlyList<Student> GetAll()
    {
        return _repository.FindAll();
    }

    public IReadOnlyList<Group> GetAllGroups()
    {
        return _repository.FindAllGroups();
    }

    public Group GetGroupByCode(string code)
    {
        return _repository.FindGroupByCode(code);
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

    public double GetAverage(Student student)
    {
        return _averageStategy.GetAverage(student);
    }

    public List<string> Validate(Student student)
    {
        return _validator.ValidateStudent(student);
    }

    public bool IsUsingStubRepo()
    {
        return _repository is StubStudentRepository;
    }

    //Repport Service Call
    public bool IsUsingLink()
    {
        return _report is ReportServiceLinq;
    }

    public List<(Student student, double avg)> GetTopAvg(int numberOfStudents = 3)
    {
        return _report.GetTopByAverage(numberOfStudents);
    }

    public List<Student> GetStudentInGroup(string gc)
    {
        return _report.GetStudentsInGroupSortedByName(gc);
    }

    public (
        int totalStudents,
        int totalGrades,
        double meanOfMeans,
        int maxGrade,
        bool hasFailing,
        bool allHaveEmail
    ) GetStatistics()
    {
        return _report.GetStatistics();
    }
}
