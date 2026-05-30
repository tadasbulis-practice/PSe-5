using CW1.Models;

namespace CW1.Interface;

public interface IStudentRepository
{
    IReadOnlyList<Student> FindAll();
    IReadOnlyList<Group> FindAllGroups();
    Group FindGroupByCode(string code);
    void Add(Student student);
    void Remove(Student student);
    Student FindById(int id);
}
