using Lab2.App.Models;

namespace Lab2.App.Services
{
    public class StudentService
    {
        public void AddGrade(Student student, int grade)
        {
            student.AddGrade(grade);
        }
    }
}
