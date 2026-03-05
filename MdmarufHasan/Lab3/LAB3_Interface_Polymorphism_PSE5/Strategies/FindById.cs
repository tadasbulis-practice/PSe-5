using System.Linq;

public class FindById : IStudentFinder
{
    public Student? Find(Group g, string q)
        => g.Students.FirstOrDefault(s => s.Id.ToString() == q);
}