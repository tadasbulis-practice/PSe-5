
using Week3.LiveCoding.Advanced.Models;

namespace Week3.LiveCoding.Advanced.Services;

public interface IStudentService
{
    void AddStudent(Group group, Student student);
    void PrintAll(Group group);
}
