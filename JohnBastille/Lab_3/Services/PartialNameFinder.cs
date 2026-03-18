public class PartialNameFinder : IStudentFinder
{
    public Student? Find(List<Student> students, string query)
    {
        return students.FirstOrDefault(s =>
            s.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
    }
}