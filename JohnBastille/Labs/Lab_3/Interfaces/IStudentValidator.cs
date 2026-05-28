public interface IStudentValidator
{
    bool Validate(string name, string ageInput, out int age);
}