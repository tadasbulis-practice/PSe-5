namespace Lab6.Legacy;

public class LegacyStudentValidation
{
    public bool CheckStudent(string name, string email)
    {
        return email.Contains("@");
    }
}
