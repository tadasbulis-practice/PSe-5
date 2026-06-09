using System.Collections.Generic;

namespace Lab7.App.Models
{
    public class Group
    {
        public string Code { get; }
        public string StudyProgram { get; }
        public int EnrollmentYear { get; }

        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyList<Student> Students => _students;

        public Group(string code, string studyProgram, int enrollmentYear)
        {
            Code = code;
            StudyProgram = studyProgram;
            EnrollmentYear = enrollmentYear;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }
    }
}