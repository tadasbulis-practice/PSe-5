using System.Collections.Generic;

namespace Lab6.App.Models
{
    public class Group
    {
        public string Name { get; }

        // Same encapsulation rule as Student: container is private,
        // the outside world sees a read-only view.
        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyList<Student> Students => _students;

        public Group(string name)
        {
            Name = name;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }
    }
}