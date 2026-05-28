using JohnBastille.Lab_5.Interfaces;
using JohnBastille.Lab_5.Models;

namespace Lab_5.Services;

public class ExactNameFinder : IStudentFinder
{
    public Student? Find(List<Student> students, string query)
    {
        return students.FirstOrDefault(s =>
            s.Name?.Equals(query, StringComparison.OrdinalIgnoreCase) == true);
    }
}



