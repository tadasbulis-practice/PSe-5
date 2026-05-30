using System.Text.Json.Serialization;

namespace Lab8.DTOs;

/// <summary>
/// Matches the full JSON returned by GET /api/faculty.
/// Structure: Faculty → Groups → Students
/// </summary>
public class ApiFacultyDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("groups")]
    public List<ApiGroupDto> Groups { get; set; } = new();
}
