using System.Text.Json.Serialization;

namespace Lab8.DTOs;

/// <summary>
/// Matches exactly the JSON shape returned by GET /api/students and
/// the students array inside GET /api/faculty.
/// This is a pure data-transfer object — no business logic here.
/// </summary>
public class ApiStudentDto
{
    [JsonPropertyName("id")]            public int    Id             { get; set; }
    [JsonPropertyName("firstName")]     public string FirstName      { get; set; } = string.Empty;
    [JsonPropertyName("lastName")]      public string LastName       { get; set; } = string.Empty;
    [JsonPropertyName("email")]         public string Email          { get; set; } = string.Empty;
    [JsonPropertyName("studyProgram")]  public string StudyProgram   { get; set; } = string.Empty;
    [JsonPropertyName("enrollmentYear")]public int    EnrollmentYear { get; set; }
}
