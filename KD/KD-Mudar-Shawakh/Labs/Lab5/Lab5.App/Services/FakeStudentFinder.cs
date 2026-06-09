using Lab5.App.Models;

namespace Lab5.App.Services
{
    public class FakeStudentFinder : IStudentFinder
    {
        public Student Find(Group g, string query)
        {
            return new Student("Fake Student");
        }
    }
}