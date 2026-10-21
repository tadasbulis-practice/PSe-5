using Lab4Demo.Models;

namespace Lab4Demo.Services;

public class StubStudentFinder : IStudentFinder
{
    private readonly Student? _result;

    public StubStudentFinder(Student? result)
    {
        _result = result;
    }

    public Student? Find(Group group, string query)
    {
        return _result;
    }
}
