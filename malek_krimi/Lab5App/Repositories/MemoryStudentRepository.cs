public class MemoryStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        return new List<Student>
        {
            new Student { Name = "Malek", Grades = new List<int> { 8, 9, 10 } },
            new Student { Name = "John", Grades = new List<int> { 6, 7, 8 } },
            new Student { Name = "Sara", Grades = new List<int> { 10, 9, 9 } }
        };
    }
}