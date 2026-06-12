using Lab6.App.Models;

namespace Lab6.App.Interfaces;

public interface IStudentRepository
{
    List<Student> GetAll();
    Student GetById(int id);
    void Add(Student student);
    bool Remove(int id);
}