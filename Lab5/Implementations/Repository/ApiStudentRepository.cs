using Lab5.Interfaces;
using Lab5.Models;

public class ApiStudentRepository : IStudentRepository
{
    public Student? Find(string query)
    {
        Console.WriteLine("Simulating API call...");
        return new Student(99, "ApiUser", "api@test.com");
    }
}
