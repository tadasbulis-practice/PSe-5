using JohnBastille.Lab_4.Models;

namespace JohnBastille.Lab_4.Interfaces
{
    public interface IStudentFinder
    {
        Student? Find(List<Student> students, string query);
    }
}



