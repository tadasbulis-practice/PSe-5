using CW1Friend.Models;

namespace CW1Friend.Interfaces;

public interface IStudentRepository
{
    List<Student> FetchAllStudents();
    Student? FetchStudentById(int id);
    void SaveStudent(Student student);
    void SaveGrade(int studentId, int grade);

    List<Group> FetchAllGroups();
    void SaveGroup(Group group);
}
