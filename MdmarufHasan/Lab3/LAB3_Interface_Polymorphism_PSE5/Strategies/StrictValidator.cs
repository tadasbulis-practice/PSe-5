public class StrictValidator : IStudentValidator
{
    public bool Validate(Student s)
        => s.Email.Contains("@") && s.Grades.Count > 0;
}