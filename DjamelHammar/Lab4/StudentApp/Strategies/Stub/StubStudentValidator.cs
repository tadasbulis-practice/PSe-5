using StudentApp.Interfaces;

namespace StudentApp.Strategies.Stub
{
    public class StubStudentValidator : IStudentValidator
    {
        public bool IsValid { get; set; } = true; // changable
        public bool Validate(int average) => IsValid;
    }
}