namespace lab1.Edgar.model;

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
    private List<double> Grades { get; }

    private double Average()
    {
        return Grades.Average();
    }

    public override string ToString()
    {
        return "name: "
            + FirstName
            + " "
            + LastName
            + ", group: "
            + Group
            + ", lecture date: "
            + LectureDate
            + "average grades : "
            + Average();
    }
}
