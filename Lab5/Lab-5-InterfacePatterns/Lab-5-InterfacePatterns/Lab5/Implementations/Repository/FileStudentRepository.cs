using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

public class FileStudentRepository : IStudentRepository
{
    public Student? Find(string query)
    {
        return new Student(2, "File Student", "file@test.com");
    }
}