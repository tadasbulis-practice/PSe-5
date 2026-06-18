public class LegacyStudentValidation
{
    public bool CheckStudent(string name, string email)
    {
        return !string.IsNullOrWhiteSpace(name) && email.Contains("@");
    }
}