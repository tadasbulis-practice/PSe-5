using CW1After.Models;

namespace CW1After.Interfaces;
// any class whent to handle repository must have this ()s
public interface IStudentRepository
{
    List<Student> GetStudents();
    List<Group> GetGroups();

    void Add(Student student);

    void UpdateStudent(Student student);
}