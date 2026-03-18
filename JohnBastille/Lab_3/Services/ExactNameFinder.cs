using JohnBastille.Lab_3.Interfaces;
using JohnBastille.Lab_3.Models;
namespace Lab_3.Services;


    public class ExactNameFinder : IStudentFinder
    {
        public Student? Find(List<Student> students, string query)
        {
            return students.FirstOrDefault(s =>
                s.Name.Equals(query, StringComparison.OrdinalIgnoreCase));
        }
    }



