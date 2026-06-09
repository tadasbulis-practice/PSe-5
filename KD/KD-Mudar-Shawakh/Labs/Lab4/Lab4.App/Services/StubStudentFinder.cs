using Lab4.App.Models;

namespace Lab4.App.Services
{
    public class StubStudentFinder : IStudentFinder
    {
        private readonly Student _result;

        public StubStudentFinder(Student result)
        {
            _result = result;
        }

        public Student Find(Group g, string query)
        {
            return _result;
        }
    }
}