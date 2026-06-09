namespace Lab7.Legacy;

// Kept from Lab-5 — simulates a legacy system we cannot modify.
// Used via the Adapter pattern.
public class LegacyStudentValidation
{
    public bool CheckStudent(string name, string email)
    {
        return !string.IsNullOrWhiteSpace(name) && email.Contains("@");
    }
}
