namespace SimpleStudentFrontend.Models;

public class StudentDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? StudyProgram { get; set; }
    public int EnrollmentYear { get; set; }
}
