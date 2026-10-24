using Lab6.Interfaces;
using Lab6.Models;
using Lab6.Legacy;

namespace Lab6.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new();

    public bool Validate(Student student)
    {
        return _legacy.CheckStudent(student.Name, student.Email);
    }

    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
    {
        return students.Where(Validate).ToList();
    }
}
