using Lab4Demo.Models;

namespace Lab4Demo.Services;

public class StudentService
{
    private readonly IStudentFinder _finder;

    public StudentService(IStudentFinder finder)
    {
        _finder = finder;
    }

    public string Search(Group group, string query)
    {
        var student = _finder.Find(group, query);

        if (student == null)
            return "Student not found";

        return $"Found: {student.Name}";
    }
}
