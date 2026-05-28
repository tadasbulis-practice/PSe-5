public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidationSystem _legacySystem;

    public StudentValidatorAdapter(LegacyStudentValidationSystem legacySystem)
    {
        _legacySystem = legacySystem;
    }

    public bool Validate(Student student)
    {
        return _legacySystem.CheckStudentData(student.Name, student.Grades);
    }

    public List<Student> ValidateAll(List<Student> students)
    {
        return students.Where(Validate).ToList();
    }
}
