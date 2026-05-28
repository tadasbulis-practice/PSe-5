using CW1After.Models;

namespace CW1After.Services;

public class StudentAverageReport
{
    public Student Student { get; set; } = new();
    public double Average { get; set; }
}
