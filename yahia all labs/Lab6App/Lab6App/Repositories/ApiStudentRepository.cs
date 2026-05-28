public class ApiStudentRepository : IStudentRepository
{
    private readonly List<Student> students = new();

    public ApiStudentRepository()
    {
        students.AddRange(new List<Student>
        {
            new Student { Id = 101, Name = "ApiStudent1", Grades = new List<int> { 7, 8, 9 } },
            new Student { Id = 102, Name = "ApiStudent2", Grades = new List<int> { 9, 9, 10 } }
        });
    }

    public List<Student> GetAll()
    {
        Console.WriteLine("Simulating API call...");
        return students.ToList();
    }

    public Student? GetById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public void Add(Student student)
    {
        students.Add(student);
    }

    public bool Remove(int id)
    {
        var student = GetById(id);
        if (student == null)
            return false;

        students.Remove(student);
        return true;
    }
}
