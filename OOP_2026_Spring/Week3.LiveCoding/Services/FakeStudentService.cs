using Week3.LiveCoding.Models;

namespace Week3.LiveCoding.Services;

public class FakeStudentService : IStudentService
{
    public void AddStudent(Group group, Student student)
    {
        Console.WriteLine("FAKE: student not really added.");
    }

    public void PrintAll(Group group)
    {
        Console.WriteLine("FAKE: no real data.");
    }
}
