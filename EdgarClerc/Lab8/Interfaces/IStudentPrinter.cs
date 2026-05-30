using Lab8.Models;

namespace Lab8.Interfaces;

public interface IStudentPrinter
{
    void PrintStudents(IReadOnlyList<Student> students);
    void PrintGroup(Group group);
    void PrintFaculty(Faculty faculty);
}
