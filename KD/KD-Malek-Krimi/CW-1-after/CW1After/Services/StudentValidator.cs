using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    private readonly IStudentRepository _repository;

    public StudentValidator(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<string> Validate(Student student)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(student.Name))
        {
            errors.Add("Name empty");
        }

        if (!student.Email.Contains('@') || !student.Email.Contains('.'))
        {
            errors.Add("Bad email");
        }

        bool groupExists = false;
        foreach (var group in _repository.GetGroups())
        {
            if (group.Code == student.GroupCode)
            {
                groupExists = true;
                break;
            }
        }

        if (!groupExists)
        {
            errors.Add("Unknown group");
        }

        foreach (int grade in student.Grades)
        {
            if (grade < 1 || grade > 10)
            {
                errors.Add($"Grade {grade} out of range");
                break;
            }
        }

        return errors;
    }
}
