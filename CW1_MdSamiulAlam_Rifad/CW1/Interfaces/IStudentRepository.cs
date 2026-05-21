using CW1After.Models;

namespace CW1After.Interfaces;

public interface IStudentRepository
{
    List<Student> GetAllStudents();
    Student? GetStudentById(int id);
    void AddStudent(Student student);
    void AddGrade(int studentId, int grade);

    List<Group> GetAllGroups();
    void AddGroup(Group group);
}
