using System.Text.Json.Serialization;

namespace Lab10.Models;

/// <summary>
/// Student domain object — same as Lab-9, with constructor-based validation.
/// Properties are read-only externally so invariants cannot be broken after creation.
/// [JsonConstructor] forces System.Text.Json to use the validating constructor.
/// </summary>
public class Student
{
    public int    Id             { get; }
    public string FirstName      { get; }
    public string LastName       { get; }
    public string Email          { get; }
    public string StudyProgram   { get; }
    public int    EnrollmentYear { get; }

    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}";

    [JsonConstructor]
    public Student(int id, string firstName, string lastName,
                   string email, string studyProgram, int enrollmentYear)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id),
                "Student ID must be a positive integer.");
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email must contain '@'.", nameof(email));
        if (string.IsNullOrWhiteSpace(studyProgram))
            throw new ArgumentException("Study program cannot be empty.", nameof(studyProgram));
        if (enrollmentYear < 2000 || enrollmentYear > 2100)
            throw new ArgumentOutOfRangeException(nameof(enrollmentYear),
                "Enrollment year must be between 2000 and 2100.");

        Id             = id;
        FirstName      = firstName;
        LastName       = lastName;
        Email          = email;
        StudyProgram   = studyProgram;
        EnrollmentYear = enrollmentYear;
    }

    public override string ToString() => $"{Id} {FullName} <{Email}>";
}
