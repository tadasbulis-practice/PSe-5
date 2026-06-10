using Lab8.Interfaces;
using Lab8.Legacy;
using Lab8.Models;

namespace Lab8.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new();

    public bool Validate(Student student)
        => _legacy.CheckStudent(student.FullName, student.Email);

    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
        => students.Where(s => Validate(s)).ToList();
}
