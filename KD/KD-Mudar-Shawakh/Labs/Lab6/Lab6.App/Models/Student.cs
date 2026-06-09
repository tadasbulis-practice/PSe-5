using System.Collections.Generic;

namespace Lab6.App.Models
{
    public class Student
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }

        // Private container — encapsulation rule from LAB-6
        private readonly List<int> _grades = new List<int>();

        // Read-only view: strategies can iterate, but they cannot modify the list
        public IReadOnlyList<int> Grades => _grades;

        public Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public void AddGrade(int grade)
        {
            _grades.Add(grade);
        }
    }
}