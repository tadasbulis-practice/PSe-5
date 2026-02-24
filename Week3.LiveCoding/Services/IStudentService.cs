using Week3.LiveCoding.Models;

namespace Week3.LiveCoding.Services;

public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}
