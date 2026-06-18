using Lab6.App.Models;

namespace Lab6.App.Interfaces;

public interface IStudentPrinter
{
    void PrintStudents(List<Student> students);
}