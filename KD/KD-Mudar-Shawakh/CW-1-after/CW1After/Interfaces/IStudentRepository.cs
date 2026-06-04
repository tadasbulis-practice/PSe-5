using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    IReadOnlyList<Student> GetAllStudents();
    IReadOnlyList<Group> GetAllGroups();
    Student? GetById(int id);
    void AddStudent(Student student);
    bool GroupExists(string code);
}