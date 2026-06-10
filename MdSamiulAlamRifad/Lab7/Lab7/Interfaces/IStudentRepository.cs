using Lab7.Models;

namespace Lab7.Interfaces;

// Task 1 — Repository with Container
public interface IStudentRepository
{
    List<Student> GetAll();
    Student? GetById(int id);
    void Add(Student student);
    bool Remove(int id);
}
