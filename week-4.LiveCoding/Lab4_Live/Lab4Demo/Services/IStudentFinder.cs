using Lab4Demo.Models;

namespace Lab4Demo.Services;

public interface IStudentFinder
{
    Student? Find(Group group, string query);
}
