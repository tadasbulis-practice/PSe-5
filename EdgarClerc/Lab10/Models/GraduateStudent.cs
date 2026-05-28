using System.Text.Json.Serialization;

namespace Lab10.Models;

/// <summary>
/// Lab-8 inheritance: GraduateStudent IS-A Student that also has a thesis title.
/// Validation chains: base ctor first, then thesis title check.
/// </summary>
public class GraduateStudent : Student
{
    public string ThesisTitle { get; }

    [JsonConstructor]
    public GraduateStudent(int id, string firstName, string lastName,
                           string email, string studyProgram, int enrollmentYear,
                           string thesisTitle)
        : base(id, firstName, lastName, email, studyProgram, enrollmentYear)
    {
        if (string.IsNullOrWhiteSpace(thesisTitle))
            throw new ArgumentException("Thesis title cannot be empty.", nameof(thesisTitle));

        ThesisTitle = thesisTitle;
    }

    public override string ToString() => $"[GRAD] {base.ToString()} — \"{ThesisTitle}\"";
}
