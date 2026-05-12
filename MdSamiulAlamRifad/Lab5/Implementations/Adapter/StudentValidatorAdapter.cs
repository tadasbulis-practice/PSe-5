
using Lab5.Interfaces;
using Lab5.Models;
using Lab5.Legacy;

namespace Lab5.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new LegacyStudentValidation();

    public bool Validate(Student student)
    {
        return _legacy.CheckStudent(student.Name, student.Email);
    }
}
