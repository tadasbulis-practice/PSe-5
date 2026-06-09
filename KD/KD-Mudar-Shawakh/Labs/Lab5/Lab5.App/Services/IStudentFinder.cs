using Lab5.App.Models;

namespace Lab5.App.Services
{
    public interface IStudentFinder
    {
        Student Find(Group g, string query);
    }
}