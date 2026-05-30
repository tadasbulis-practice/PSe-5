using Lab9.Models;

namespace Lab9.Interfaces;

public interface IStudentPrinter
{
    void PrintStudents(IReadOnlyList<Student> students);
    void PrintGroup(Group group);
    void PrintFaculty(Faculty faculty);
}
