using System.Collections.Generic;
using Lab6.App.Models;

namespace Lab6.App.Interfaces
{
    public interface IStudentValidator
    {
        // Validate one student — used when adding a new student.
        bool Validate(Student student);

        // Validate a collection — returns ONLY the students that pass.
        // The implementation must filter, not just check; this is what
        // StudentService calls before calculating a group average.
        IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students);
    }
}