using Lab8.Interfaces;
using Lab8.Models;

namespace Lab8.Implementations.Validator;

// SOLID — Liskov Substitution Principle (LSP)
// StrictStudentValidator can replace StudentValidatorAdapter anywhere
// IStudentValidator is used — the system keeps working correctly.
// It adds stricter rules without breaking the interface contract.
public class StrictStudentValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.FirstName)) return false;
        if (string.IsNullOrWhiteSpace(student.LastName))  return false;
        if (!student.Email.Contains('@'))                  return false;
        if (!student.Email.Contains('.'))                  return false;
        if (student.EnrollmentYear < 2000)                return false;
        if (student.EnrollmentYear > DateTime.Now.Year)   return false;
        return true;
    }

    public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
        => students.Where(Validate).ToList();
}
