using Lab4.App.Models;

namespace Lab4.App.Services
{
    public interface IStudentFinder
    {
        Student Find(Group g, string query);
    }
}