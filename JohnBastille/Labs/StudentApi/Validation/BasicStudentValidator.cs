using StudentApi.Interfaces;

namespace StudentApi.Validation;

public class BasicStudentValidator : IStudentValidator
{
    public bool IsValid(string name, int age, List<int> grades, out string? error)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Name is required.";
            return false;
        }

        if (age < 5 || age > 120)
        {
            error = "Age must be between 5 and 120.";
            return false;
        }

        if (grades.Any(g => g < 0 || g > 10))
        {
            error = "Grades must be between 0 and 10.";
            return false;
        }

        error = null;
        return true;
    }
}
