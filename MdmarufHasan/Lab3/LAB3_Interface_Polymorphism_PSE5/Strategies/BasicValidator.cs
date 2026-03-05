public class BasicValidator : IStudentValidator
{
    public bool Validate(Student s)
        => !string.IsNullOrWhiteSpace(s.Name);
}