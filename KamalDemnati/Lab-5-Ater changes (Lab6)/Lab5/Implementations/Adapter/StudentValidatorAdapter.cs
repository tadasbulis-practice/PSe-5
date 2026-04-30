
using Lab5.Interfaces;
using Lab5.Models;
using Lab5.Legacy;

namespace Lab5.Implementations.Adapter;

using System.Collections.Generic;
using System.Linq;

public class StudentValidatorAdapter : IStudentValidator
{
    private readonly StudentValidator _legacyValidator;

    public StudentValidatorAdapter(StudentValidator legacyValidator)
    {
        _legacyValidator = legacyValidator;
    }

    public bool Validate(Student student)
    {
        return _legacyValidator.Validate(student);
    }

    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(s => Validate(s)).ToList();
    }
}
