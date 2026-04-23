public class ApiStudentRepository : IStudentRepository
{
    public List<Student> GetAll()
    {
        Console.WriteLine("Simulating API call...");

        return new List<Student>
        {
            new Student { Name = "ApiStudent1", Grades = new List<int> { 7, 8, 9 } },
            new Student { Name = "ApiStudent2", Grades = new List<int> { 9, 9, 10 } }
        };
    }
}