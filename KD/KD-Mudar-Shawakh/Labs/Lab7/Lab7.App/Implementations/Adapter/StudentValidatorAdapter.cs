using Lab7.App.Interfaces;
using Lab7.App.Legacy;
using Lab7.App.Models;

namespace Lab7.App.Implementations.Adapter
{
    /// <summary>
    /// Adapter pattern: wraps the legacy CheckStudent(string, string) API
    /// behind the modern Validate(Student) contract.
    ///
    /// StudentService never knows that a legacy library exists — it only
    /// sees IStudentValidator.
    /// </summary>
    public class StudentValidatorAdapter : IStudentValidator
    {
        private readonly LegacyStudentValidation _legacy;

        public StudentValidatorAdapter()
        {
            // Owns the legacy instance. Could also be injected for testability;
            // owning it here keeps Program.cs simpler.
            _legacy = new LegacyStudentValidation();
        }

        public bool Validate(Student student)
        {
            // Translate from the modern Student shape into the legacy signature.
            return _legacy.CheckStudent(student.FullName, student.Email);
        }
    }
}