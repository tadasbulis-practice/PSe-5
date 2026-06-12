public class StrictStudentValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return student.Email.Contains("@") && student.Grades.Count >= 3;
    }
}