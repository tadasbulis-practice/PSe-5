using StudentApp.Interfaces;

namespace StudentApp.Strategies.Real
{
    public class StrictStudentValidator : IStudentValidator
    {
        public bool Validate(int average)
        {
            return average >= 12;
        }
    }
}