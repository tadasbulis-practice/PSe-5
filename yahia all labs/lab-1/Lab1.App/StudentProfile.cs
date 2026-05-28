public class StudentProfile
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Group { get; set; } = "";
    public DateOnly LectureDate { get; set; }

    public string ToReadmeBlock(int taskNo, int? fallbackTask = null)
    {
        string result =
$@"## Lab 1

**Name Surname:** {FirstName} {LastName}  
**Group:** {Group}  
**Lecture date:** {LectureDate}  
**Random task:** {taskNo}";

        if (fallbackTask != null)
        {
            result += $@"
**Fallback (if #5 is too hard):** {fallbackTask}";
        }

        result += @"

### Run
dotnet run --project Lab1.App

### Implemented
- Task solution goes here
";

        return result;
    }
}
