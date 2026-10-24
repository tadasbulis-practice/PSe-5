namespace Lab7.Legacy;

// Pretend this is old code we cannot change
public class LegacyStudentValidation
{
    public bool CheckStudent(string name, string email)
        => !string.IsNullOrWhiteSpace(name) && email.Contains('@');
}
