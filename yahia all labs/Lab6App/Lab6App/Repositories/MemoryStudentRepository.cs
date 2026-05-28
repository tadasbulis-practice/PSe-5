public class MemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> students = new();

    public List<Student> GetAll()
    {
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
