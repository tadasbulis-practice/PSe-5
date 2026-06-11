using Lab7.Interfaces;
using Lab7.Legacy;
using Lab7.Models;

namespace Lab7.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new();

    public bool Validate(Student student)
        => _legacy.CheckStudent(student.FullName, student.Email);

    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
        => students.Where(s => Validate(s)).ToList();
}
