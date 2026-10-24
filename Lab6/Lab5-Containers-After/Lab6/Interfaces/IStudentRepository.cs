using Lab6.Models;

namespace Lab6.Interfaces;

public interface IStudentRepository
{
    IReadOnlyList<Student> GetAll();
    Student? FindById(int id);
    void Add(Student student);
    bool Remove(int id);
}
