using CW1.Interface;
using CW1.Models;

namespace CW1.Services;

public class StudentValidator
{
    private readonly IStudentRepository _repository;

    public StudentValidator(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<string> ValidateStudent(Student student)
    {
        var errors = new List<string>();

        // 3. Core Business Rule Validations
        if (!student.Email.Contains('@') || !student.Email.Contains('.'))
        {
            errors.Add("Bad email");
        }

        try
        {
            _repository.FindGroupByCode(student.GroupCode);
        }
        catch
        {
            errors.Add("Unknown group");
        }

        foreach (var gr in student.Grades)
        {
            if (gr < 1 || gr > 10)
            {
                errors.Add($"Grade {gr} out of range");
                break;
            }
        }

        return errors;
    }
}
