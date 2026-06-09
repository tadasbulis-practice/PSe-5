using System.Collections.Generic;

namespace Lab7.App.Models
{
    public class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string StudyProgram { get; }
        public int EnrollmentYear { get; }

        // Computed property — no setter, derived from the two name fields.
        public string FullName => $"{FirstName} {LastName}";

        // Same encapsulation pattern as LAB-6: private container, read-only view.
        private readonly List<int> _grades = new List<int>();
        public IReadOnlyList<int> Grades => _grades;

        public Student(int id, string firstName, string lastName,
                       string email, string studyProgram, int enrollmentYear)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            StudyProgram = studyProgram;
            EnrollmentYear = enrollmentYear;
        }

        public void AddGrade(int grade)
        {
            _grades.Add(grade);
        }
    }
}