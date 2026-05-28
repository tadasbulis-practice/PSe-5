using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1.Models;
using CW1.Interfaces;

namespace CW1.Services
{
    public class StudentValidator
    {
        public List<string> Validate(Student student, List<Group> groups)
        {
            List<string> errors = new();

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                errors.Add("Name empty");
            }

            if (!student.Email.Contains('@') || !student.Email.Contains('.'))
            {
                errors.Add("Bad email");
            }

            bool groupExists = false;

            foreach (var g in groups)
            {
                if (g.Code == student.GroupCode)
                {
                    groupExists = true;
                    break;
                }
            }

            if (!groupExists)
            {
                errors.Add("Unknown group");
            }

            foreach (int grade in student.Grades)
            {
                if (grade < 1 || grade > 10)
                {
                    errors.Add($"Grade {grade} out of range");
                    break;
                }
            }

            return errors;
        }
    }
}
