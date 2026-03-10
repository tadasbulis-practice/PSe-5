using Lab-3-JohnBastille.Models;

namespace Lab-3-JohnBastille.Interfaces 

public interface IStudentFinder
{
    Student? Find(IReadOnlyCollection<Student> students);
}

