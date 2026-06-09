using System.Collections.Generic;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class StudentValidator : IStudentValidator
    {
        public bool Validate(Student student)
        {
            // Same two rules I used in LAB-4 / LAB-5 input checking,
            // now living in one dedicated class instead of inside Program.cs.
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return false;
            }

            if (!student.Email.Contains('@'))
            {
                return false;
            }

            return true;
        }

        public IReadOnlyList<Student> ValidateAll(IReadOnlyList<Student> students)
        {
            // Filter — return only students that pass Validate.
            // This is what StudentService calls before calculating a group average,
            // so invalid students don't pollute the result.
            var valid = new List<Student>();

            foreach (var s in students)
            {
                if (Validate(s))
                {
                    valid.Add(s);
                }
            }

            return valid;
        }
    }
}