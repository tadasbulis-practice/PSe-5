public class CaseInsensitiveSearch : IStudentFinder
{
    public Student Find(List<Student> students, string name)
    {
        return students.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
    }
}