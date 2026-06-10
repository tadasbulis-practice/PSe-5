using Lab7.Interfaces;
using Lab7.Legacy;
using Lab7.Models;

namespace Lab7.Implementations.Validator;

// Task 4 — Validation for Collections
// Uses Adapter pattern to wrap LegacyStudentValidation.
// ValidateAll filters out any students that fail the single Validate check.
public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy = new();

    public bool Validate(Student student)
    {
        return _legacy.CheckStudent(student.Name, student.Email);
    }

    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(Validate).ToList();
    }
}
