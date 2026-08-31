public class LinearSearch : IStudentFinder
{
    public Student Find(List<Student> students, string name)
    {
        return students.FirstOrDefault(s => s.Name == name);
    }
}