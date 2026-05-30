namespace Lab7.Models;

using Interfaces;

public class Group : IEntity
{
    public int Id { get; }
    public string Code { get; }
    public string StudyProgram { get; }
    public int EnrollmentYear { get; }

    private readonly List<Student> _students = new();
    public IReadOnlyList<Student> Students => _students;

    public Group(int id, string code, string studyProgram, int enrollmentYear)
    {
        Id = id;
        Code = code;
        StudyProgram = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    // Called only by Repository during initialization
    public void AddStudent(Student student) => _students.Add(student);

    public void RemoveStudent(Student student) => _students.Remove(student);
}
