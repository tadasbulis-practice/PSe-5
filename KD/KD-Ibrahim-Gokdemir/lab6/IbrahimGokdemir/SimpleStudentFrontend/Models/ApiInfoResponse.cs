namespace SimpleStudentFrontend.Models;

public class ApiInfoResponse
{
    public string? Service { get; set; }
    public string? Version { get; set; }
    public string? Status { get; set; }
    public string? Message { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string? Environment { get; set; }
}
