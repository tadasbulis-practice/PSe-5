using CW1After.Interfaces;
using CW1After.Models;

namespace CW1After.Services;


/// Task 3
/// Returns exactly 1 group ("TEST") and 1 student (Id=999, "Test Student").
/// Add() is intentionally not supported; stubs do not persist data.

public class StubStudentRepository : IStudentRepository
{
    private readonly List<Group>   _groups;
    private readonly List<Student> _students;

    public StubStudentRepository()
    {
        _groups = new List<Group>
        {
            new Group { Code = "TEST", Name = "Test group" }
        };
        _students = new List<Student>
        {
            new Student
            {
                Id        = 999,
                Name      = "Test Student",
                Email     = "test@test.lt",
                GroupCode = "TEST",
                Grades    = new List<int> { 10, 10, 10 }
            }
        };
    }

    public IReadOnlyList<Student> GetAllStudents() => _students.AsReadOnly();
    public IReadOnlyList<Group>   GetAllGroups()   => _groups.AsReadOnly();
    public Student?               GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void AddStudent(Student student) =>
        throw new NotSupportedException("StubStudentRepository does not support adding students.");

    public void AddGrade(int studentId, int grade)
    {
    }
}
