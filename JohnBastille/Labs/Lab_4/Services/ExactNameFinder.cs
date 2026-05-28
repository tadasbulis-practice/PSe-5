using JohnBastille.Lab_4.Interfaces;
using JohnBastille.Lab_4.Models;

namespace Lab_4.Services;

public class ExactNameFinder : IStudentFinder
{
    public Student? Find(List<Student> students, string query)
    {
        return students.FirstOrDefault(s =>
            s.Name?.Equals(query, StringComparison.OrdinalIgnoreCase) == true);
    }
}



