using System.Linq;

public class FindByName : IStudentFinder
{
    public Student? Find(Group g, string q)
        => g.Students.FirstOrDefault(s => s.Name == q);
}