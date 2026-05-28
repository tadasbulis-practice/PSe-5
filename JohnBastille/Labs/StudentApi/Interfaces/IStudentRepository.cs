using StudentApi.Models;

namespace StudentApi.Interfaces;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    void Add(Student student);
}
