namespace Lab9.Models;

public class GraduateStudent : Student
{
    public string ThesisTitle { get; }

    public GraduateStudent(
        int id,
        string firstName,
        string lastName,
        string email,
        string studyProgram,
        int year,
        string thesisTitle
    )
        : base(id, firstName, lastName, email, studyProgram, year)
    {
        if (thesisTitle.Length < 1)
        {
            throw new ArgumentException(
                "Thesis title must be at least 1 character long",
                thesisTitle
            );
        }
        ThesisTitle = thesisTitle;
    }

    public override string Info() => $"{base.Info()} | Thesis: {ThesisTitle}";
}
