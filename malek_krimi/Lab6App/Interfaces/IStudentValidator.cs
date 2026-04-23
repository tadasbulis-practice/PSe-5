public interface IStudentValidator
{
    bool Validate(Student student);
    List<Student> ValidateAll(List<Student> students);
}
