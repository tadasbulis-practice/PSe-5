public class LegacyStudentValidationSystem
{
    public bool CheckStudentData(string name, List<int> grades)
    {
        return !string.IsNullOrWhiteSpace(name) &&
               grades != null &&
               grades.Count > 0 &&
               grades.All(g => g >= 1 && g <= 10);
    }
}