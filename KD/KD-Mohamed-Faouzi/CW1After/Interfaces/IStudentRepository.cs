using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    List<Student> GetAllStudents();
    List<Group> GetAllGroups();
    Student? GetById(int id);
    void Add(Student student);
}