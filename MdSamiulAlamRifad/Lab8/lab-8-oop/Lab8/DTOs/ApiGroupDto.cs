using System.Text.Json.Serialization;

namespace Lab8.DTOs;

/// <summary>
/// Matches the group object inside GET /api/faculty response.
/// </summary>
public class ApiGroupDto
{
    [JsonPropertyName("code")]           public string           Code           { get; set; } = string.Empty;
    [JsonPropertyName("studyProgram")]   public string           StudyProgram   { get; set; } = string.Empty;
    [JsonPropertyName("enrollmentYear")] public int              EnrollmentYear { get; set; }
    [JsonPropertyName("students")]       public List<ApiStudentDto> Students    { get; set; } = new();
}
