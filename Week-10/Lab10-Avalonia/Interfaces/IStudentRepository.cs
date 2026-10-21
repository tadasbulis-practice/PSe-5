using Lab10.Models;

namespace Lab10.Interfaces;

/// <summary>
/// Same contract as Lab-7..9. The form should depend only on this interface,
/// never on concrete Memory or Api implementations.
/// </summary>
public interface IStudentRepository
{
    IReadOnlyList<Student> GetAll();
    Student? GetById(int id);
    void     Add(Student student);
    bool     Remove(int id);
}
