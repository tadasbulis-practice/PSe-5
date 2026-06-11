using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class StrictValidator : IStudentValidator
    {
        public bool Validate(Student student)
            => student.Email.Contains("@") && student.AverageGrade >= 5;

        public List<Student> ValidateAll(List<Student> students)
            => students.Where(Validate).ToList();
    }
}