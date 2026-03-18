using StudentApp.Interfaces;

namespace StudentApp.Strategies.Fake
{
    public class FakeStudentValidator : IStudentValidator
    {
        public bool Validate(int average) => true; // always valid
    }
}