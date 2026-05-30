using Lab7.Models;

namespace Lab7.Interfaces;

public interface IStudentPrinter
{
    void PrintStudents(IReadOnlyList<Student> students);
    void PrintGroup(Group group);
    void PrintFaculty(Faculty faculty);
}
