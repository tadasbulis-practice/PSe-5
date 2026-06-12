using Lab4Demo.Models;

namespace Lab4Demo.Services;

public class FakeStudentFinder : IStudentFinder
{
    public Student? Find(Group group, string query)
    {
        return new Student(1, "Fake Student", "fake@test.com");
    }
}
