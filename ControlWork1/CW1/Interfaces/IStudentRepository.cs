using CW1After.Models;

namespace CW1After.Interfaces;

/// Abstraction over any student data source (memory, stub, database, …).
/// StudentService depends on this interface, not on a concrete class.
public interface IStudentRepository
{
    IReadOnlyList<Student> GetAllStudents();
    IReadOnlyList<Group>   GetAllGroups();
    Student?               GetStudentById(int id);
    void                   AddStudent(Student student);
    void                   AddGrade(int studentId, int grade);
}
