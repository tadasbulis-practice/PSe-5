using Lab6.Interfaces;
using Lab6.Models;

namespace Lab6.Implementations.Repository;

public class MemoryStudentRepository : IStudentRepository
{
    private List<Student> _studentList = [];

    public Student Add(Student student)
    {
        _studentList.Add(student);
        //if on real database return with updated id
        return student;
    }

    public void Remove(Student student)
    {
        _studentList.Remove(student);
    }

    public Student? FindById(int id)
    {
        return _studentList.Find(s => s.Id == id);
    }

    public IReadOnlyList<Student> GetAll()
    {
        return _studentList.AsReadOnly();
    }

    public Student? Find(string query)
    {
        return _studentList.Find(s => s.Name.Contains(query) || s.Email.Contains(query));
    }

    public void AddGrade(Student student, int grade)
    {
        FindById(student.Id)?.AddGrade(grade);
    }
}
