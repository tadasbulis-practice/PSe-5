using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;

namespace Lab_3.Services;

/// <summary>
/// Fake implementation of IStudentFinder for testing.
/// Always returns a predefined student regardless of query.
/// </summary>
public class FakeStudentFinder : IStudentFinder
{
    private readonly Student _fakeStudent;

    public FakeStudentFinder(Student fakeStudent)
    {
        _fakeStudent = fakeStudent;
    }

    public Student? Find(List<Student> students, string query)
    {
        // Always return the fake student for testing
        return _fakeStudent;
    }
}