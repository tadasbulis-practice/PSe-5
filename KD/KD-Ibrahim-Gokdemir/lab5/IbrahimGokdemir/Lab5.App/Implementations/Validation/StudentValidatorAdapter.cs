public class StudentValidatorAdapter : IStudentValidator
{
    private readonly LegacyStudentValidation _legacy;

    public StudentValidatorAdapter(LegacyStudentValidation legacy)
    {
        _legacy = legacy;
    }

    public bool Validate(Student student)
    {
        return _legacy.CheckStudent(student.Name, student.Email);
    }
}