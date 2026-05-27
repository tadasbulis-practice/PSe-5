namespace Lab8.Models;

using Interfaces;

public class Group
{
    public string Code { get; }
    public string StudyProgram { get; }
    public int EnrollmentYear { get; }

    private readonly List<Student> _students = new();
    public IReadOnlyList<Student> Students => _students;

    public Group(string code, string studyProgram, int enrollmentYear)
    {
        Code = code;
        StudyProgram = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    // Called only by Repository during initialization
    public void AddStudent(Student student) => _students.Add(student);

    public void RemoveStudent(Student student) => _students.Remove(student);
}
