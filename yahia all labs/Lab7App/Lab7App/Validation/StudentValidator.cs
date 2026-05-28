public class StudentValidator : IStudentValidator
{
    public bool Validate(Student student)
    {
        return !string.IsNullOrWhiteSpace(student.Name)
               && student.Grades != null
               && student.Grades.Count > 0
               && student.Grades.All(g => g >= 1 && g <= 10);
    }

    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(Validate).ToList();
    }
}
