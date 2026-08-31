public class StrictValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return !string.IsNullOrEmpty(student.Name) && student.Grades.Count > 0;
    }
}