using CW1Friend.Interfaces;
using CW1Friend.Models;

namespace CW1Friend.Services;

public class StudentService
{
    private readonly IStudentRepository _repo;
    private readonly AverageCalculator _calc;
    private readonly StudentValidator _validator;

    public StudentService(IStudentRepository repo, AverageCalculator calc, StudentValidator validator)
    {
        _repo      = repo;
        _calc      = calc;
        _validator = validator;
    }

    public List<Student> ListAll() => _repo.FetchAllStudents();

    public void AddStudent(Student s) => _repo.SaveStudent(s);

    public bool AddGrade(int studentId, int grade)
    {
        var s = _repo.FetchStudentById(studentId);
        if (s == null) return false;
        _repo.SaveGrade(studentId, grade);
        return true;
    }

    public double GetAverage(int studentId)
    {
        var s = _repo.FetchStudentById(studentId);
        if (s == null) return -1;
        return _calc.GetAverage(s);
    }

    public Student? FindStudent(int id) => _repo.FetchStudentById(id);

    public List<string> Validate(int studentId)
    {
        var s = _repo.FetchStudentById(studentId);
        if (s == null) return new List<string> { "No student found with that ID." };
        return _validator.CheckStudent(s);
    }

    public List<Group> ListGroups() => _repo.FetchAllGroups();

    public int NextId()
    {
        var all = _repo.FetchAllStudents();
        int max = 0;
        for (int i = 0; i < all.Count; i++)
            if (all[i].Id > max) max = all[i].Id;
        return max + 1;
    }
}
