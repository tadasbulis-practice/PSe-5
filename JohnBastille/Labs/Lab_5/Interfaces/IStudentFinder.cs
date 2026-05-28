using JohnBastille.Lab_5.Models;

namespace JohnBastille.Lab_5.Interfaces
{
    public interface IStudentFinder
    {
        Student? Find(List<Student> students, string query);
    }
}



