namespace Lab8.Models;

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
        ThesisTitle = thesisTitle;
    }

    public override string Info() => $"{base.Info()} | Thesis: {ThesisTitle}";
}
