using Lab7.App.Models;

namespace Lab7.App.Interfaces
{
    public interface IStudentValidator
    {
        // Lab 7's adapter only needs single-student validation,
        // because the legacy system did not support batch operations.
        bool Validate(Student student);
    }
}