namespace Lab_5.Services;

public class BasicStudentValidator : IStudentValidator
{
    public bool Validate(string name, string ageInput, out int age)
    {
        age = 0;

        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (!int.TryParse(ageInput, out age))
            return false;

        if (age < 0 || age > 120)
            return false;

        return true;
    }
}

public class StrictStudentValidator : IStudentValidator
{
    public bool Validate(string name, string ageInput, out int age)
    {
        age = 0;

        // Name must be at least 3 characters
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            return false;

        // Age must be a number
        if (!int.TryParse(ageInput, out age))
            return false;

        // Age must be between 18 and 100
        if (age < 18 || age > 100)
            return false;

        return true;
    }
}