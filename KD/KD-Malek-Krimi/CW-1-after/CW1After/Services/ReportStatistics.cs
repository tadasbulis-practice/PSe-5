namespace CW1After.Services;

public class ReportStatistics
{
    public int TotalStudents { get; set; }
    public int TotalGrades { get; set; }
    public double MeanOfAverages { get; set; }
    public int MaxGrade { get; set; }
    public bool HasFailing { get; set; }
    public bool AllHaveEmail { get; set; }
}
