public class BasicValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return !string.IsNullOrEmpty(student.Name);
    }
}