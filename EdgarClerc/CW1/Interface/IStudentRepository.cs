using CW1.Models;

namespace CW1.Interface;

public interface IStudentRepository
{
    IReadOnlyList<Student> FindAll();
}
