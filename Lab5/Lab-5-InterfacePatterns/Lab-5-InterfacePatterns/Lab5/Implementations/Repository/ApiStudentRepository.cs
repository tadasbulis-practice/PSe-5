using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

public class ApiStudentRepository : IStudentRepository
{
    public Student? Find(string query)
    {
        return new Student(3, "API Student", "api@test.com");
    }
}