using System.Collections.Generic;

public class MemoryStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        return new List<Student>
        {
            new Student(1, "Alice", "alice@test.com", new List<int> { 8, 9, 10 }),
            new Student(2, "Bob", "bob@test.com", new List<int> { 6, 7, 8 }),
            new Student(3, "Charlie", "charlie@test.com", new List<int> { 9, 9, 10 })
        };
    }
}