using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public interface IStudentValidator
    {
        bool Validate(Student student);
        List<Student> ValidateAll(List<Student> students);
    }
}