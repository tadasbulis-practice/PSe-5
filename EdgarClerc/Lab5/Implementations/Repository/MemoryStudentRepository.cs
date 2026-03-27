
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Repository;

public class MemoryStudentRepository : IStudentRepository
{
    public Student? Find(string query)
    {
        return new Student(1, "Demo Student", "demo@test.com");
    }
}
