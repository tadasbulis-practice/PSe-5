namespace Lab9.Models;

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

    public Student(
        int id,
        string firstName,
        string lastName,
        string email,
        string studyProgram,
        int enrollmentYear
    )
    {
        if (id < 0)
        {
            throw new ArgumentException("Id must be positive", id.ToString());
        }
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        StudyProgram = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    public void AddGrade(int grade) => _grades.Add(grade);

    public virtual string Info() => $"Student: {FullName}";
}
