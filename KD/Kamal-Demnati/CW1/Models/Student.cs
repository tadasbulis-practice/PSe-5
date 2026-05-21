using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1.Models
{
    public class Student
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string GroupCode { get; }

        private readonly List<int> _grades = new();
        public IReadOnlyList<int> Grades => _grades;

        public Student(int id, string name, string email, string groupCode)
        {
            Id = id;
            Name = name;
            Email = email;
            GroupCode = groupCode;
        }

        public void AddGrade(int grade)
        {
            _grades.Add(grade);
        }
    }
}
