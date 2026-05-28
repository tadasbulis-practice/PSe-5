using Lab6.Interfaces;
using Lab6.Legacy;
using Lab6.Models;

namespace Lab6.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new LegacyStudentValidation();

    public bool Validate(Student student)
    {
        return _legacy.CheckStudent(student.Name, student.Email);
    }
}
