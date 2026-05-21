using CW1After.Models;

namespace CW1After.Services;

public class StudentValidator
{
    public List<string> Validate(Student student)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(student.Name))
            errors.Add("Name is required.");

        if (string.IsNullOrWhiteSpace(student.Email))
            errors.Add("Email is required.");
        else if (!student.Email.Contains("@"))
            errors.Add("Email must contain '@'.");

        if (string.IsNullOrWhiteSpace(student.GroupCode))
            errors.Add("GroupCode is required.");

        foreach (var grade in student.Grades)
        {
            if (grade < 1 || grade > 10)
            {
                errors.Add($"Grade {grade} is out of range (1–10).");
                break;
            }
        }

        return errors;
    }
}
