namespace StudentApi.Interfaces;

public interface IStudentValidator
{
    bool IsValid(string name, int age, List<int> grades, out string? error);
}
