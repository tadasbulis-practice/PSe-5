public class StrictValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return !string.IsNullOrWhiteSpace(student.Name) &&
               student.Grades != null &&
               student.Grades.Count > 0;
    }
}
