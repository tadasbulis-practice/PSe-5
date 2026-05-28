using CW1.Models;

namespace CW1.Interface;

public interface IStudentRepository
{
    IReadOnlyList<Student> FindAll();
    void Add(Student student);
    void Remove(Student student);
    Student? FindById(int id);
}
