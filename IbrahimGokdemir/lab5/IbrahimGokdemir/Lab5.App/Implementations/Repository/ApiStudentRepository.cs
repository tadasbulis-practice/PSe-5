using System.Collections.Generic;

public class ApiStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        return new List<Student>
        {
            new Student(101, "ApiAlice", "apialice@test.com", new List<int> { 10, 8, 9 }),
            new Student(102, "ApiBob", "apibob@test.com", new List<int> { 7, 7, 8 })
        };
    }
}