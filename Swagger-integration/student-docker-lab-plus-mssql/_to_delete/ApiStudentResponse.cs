namespace SimpleStudentFrontend.Models;

public class ApiStudentResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Status { get; set; }
    public string? Message { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string? Environment { get; set; }
}
