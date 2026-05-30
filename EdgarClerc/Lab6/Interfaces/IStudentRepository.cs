using Lab6.Models;

namespace Lab6.Interfaces;

public interface IStudentRepository
{
    Student? Find(string query);
    IReadOnlyList<Student> GetAll();
    Student Add(Student student);
    void Remove(Student student);
    Student? FindById(int id);
    void AddGrade(Student student, int grade);
}
