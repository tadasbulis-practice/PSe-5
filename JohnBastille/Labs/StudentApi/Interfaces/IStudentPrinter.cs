using StudentApi.Models;

namespace StudentApi.Interfaces;

public interface IStudentPrinter
{
    string Format(Student student, double average);
}
