using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class LenientValidator : IStudentValidator
    {
        public bool Validate(Student student)
            => student.Email.Contains("@");

        public List<Student> ValidateAll(List<Student> students)
            => students.Where(Validate).ToList();
    }
}