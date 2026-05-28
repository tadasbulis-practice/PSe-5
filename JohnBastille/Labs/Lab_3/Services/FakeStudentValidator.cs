using JohnBastille.Lab_3.Interfaces;

namespace Lab_3.Services;

/// <summary>
/// Fake implementation of IStudentValidator for testing.
/// Can be configured to always return true or false.
/// </summary>
public class FakeStudentValidator : IStudentValidator
{
    private readonly bool _alwaysValid;
    private readonly int _fixedAge;

    public FakeStudentValidator(bool alwaysValid, int fixedAge = 20)
    {
        _alwaysValid = alwaysValid;
        _fixedAge = fixedAge;
    }

    public bool Validate(string name, string ageInput, out int age)
    {
        age = _fixedAge;
        return _alwaysValid;
    }
}