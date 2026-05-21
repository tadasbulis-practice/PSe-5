using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1.Models
{
    public class Group
    {
        public string Code { get; }
        public string Name { get; }

        private readonly List<Student> _students = new();
        public IReadOnlyList<Student> Students => _students;

        public Group(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }
    }
}
