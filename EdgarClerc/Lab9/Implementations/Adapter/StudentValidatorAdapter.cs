using Lab9.Interfaces;
using Lab9.Legacy;
using Lab9.Models;

namespace Lab9.Implementations.Adapter;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new();

    public bool Validate(Student student) => _legacy.CheckStudent(student.FullName, student.Email);

    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students) =>
        students.Where(s => Validate(s)).ToList();
}
