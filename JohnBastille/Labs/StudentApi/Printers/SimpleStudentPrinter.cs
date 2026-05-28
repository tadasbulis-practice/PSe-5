using StudentApi.Interfaces;
using StudentApi.Models;

namespace StudentApi.Printers;

public class SimpleStudentPrinter : IStudentPrinter
{
    public string Format(Student student, double average)
    {
        return $"{student.Id}: {student.Name} (Age {student.Age}) – Avg: {average:F2}";
    }
}
