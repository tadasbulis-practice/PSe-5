using System.Linq;
using Lab4Demo.Models;

namespace Lab4Demo.Services;

public class FindById : IStudentFinder
{
    public Student? Find(Group group, string query)
    {
        return group.Students
            .FirstOrDefault(s => s.Id.ToString() == query);
    }
}
