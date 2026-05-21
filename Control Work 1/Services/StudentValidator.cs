using CW1Friend.Models;

namespace CW1Friend.Services;

public class StudentValidator
{
    public List<string> CheckStudent(Student student)
    {
        var problems = new List<string>();

        CheckName(student.FullName, problems);
        CheckEmail(student.EmailAddress, problems);
        CheckGroup(student.GroupCode, problems);
        CheckGrades(student.Grades, problems);

        return problems;
    }

    private void CheckName(string name, List<string> problems)
    {
        if (string.IsNullOrWhiteSpace(name))
            problems.Add("Full name cannot be empty.");
    }

    private void CheckEmail(string email, List<string> problems)
    {
        if (string.IsNullOrWhiteSpace(email))
            problems.Add("Email cannot be empty.");
        else if (!email.Contains("@"))
            problems.Add("Email must contain '@'.");
    }

    private void CheckGroup(string code, List<string> problems)
    {
        if (string.IsNullOrWhiteSpace(code))
            problems.Add("Group code cannot be empty.");
    }

    private void CheckGrades(List<int> grades, List<string> problems)
    {
        for (int i = 0; i < grades.Count; i++)
        {
            if (grades[i] < 1 || grades[i] > 10)
            {
                problems.Add($"Grade {grades[i]} is invalid. Must be between 1 and 10.");
                break;
            }
        }
    }
}
