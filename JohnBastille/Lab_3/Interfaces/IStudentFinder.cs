using JohnBastille.Lab_3.Models;

namespace JohnBastille.Lab_3.Interfaces
{
    public interface IStudentFinder
    {
        Student? Find(List<Student> students, string query);
    }
}



