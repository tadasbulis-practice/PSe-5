namespace Lab7.App.Legacy
{
    /// <summary>
    /// Pretend this class lives in an old library we cannot modify.
    /// Its API takes raw strings, not a Student object — so it is
    /// incompatible with IStudentValidator. The Adapter bridges that.
    /// </summary>
    public class LegacyStudentValidation
    {
        public bool CheckStudent(string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return false;
            }

            if (!email.Contains('@'))
            {
                return false;
            }

            return true;
        }
    }
}