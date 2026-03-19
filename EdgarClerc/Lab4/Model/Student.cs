namespace lab3.model;

public class StudentProfile
{
    public StudentProfile(
        string firstName,
        string lastName,
        string group,
        DateOnly lectureDate,
        List<double> grades
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Group = group;
        LectureDate = lectureDate;
        Grades = grades;
    }

    private string FirstName { get; }
    private string LastName { get; }
    private string Group { get; }
    private DateOnly LectureDate { get; }
    public List<double> Grades { get; }

    public override string ToString()
    {
        return "name: "
            + FirstName
            + " "
            + LastName
            + ", group: "
            + Group
            + ", lecture date: "
            + LectureDate;
    }
}
