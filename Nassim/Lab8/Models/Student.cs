namespace Lab7.Models;

public class Student
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; }
    public string StudyProgram { get; }
    public int EnrollmentYear { get; }

    private readonly List<int> _grades = new();
    public IReadOnlyList<int> Grades => _grades;
    public virtual string Info()
        => $"Student: {FullName}";
    public Student(int id, string firstName, string lastName,
                   string email, string studyProgram, int enrollmentYear)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        StudyProgram = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    public void AddGrade(int grade) => _grades.Add(grade);
}
