using Lab4.App.Models;

namespace Lab4.App.Services
{
    public class FakeStudentFinder : IStudentFinder
    {
        public Student Find(Group g, string query)
        {
            return new Student("Fake Student");
        }
    }
}