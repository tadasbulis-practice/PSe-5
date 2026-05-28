using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    List<Student> GetStudents();

    List<Group> GetGroups();

    void Add(Student student);
}