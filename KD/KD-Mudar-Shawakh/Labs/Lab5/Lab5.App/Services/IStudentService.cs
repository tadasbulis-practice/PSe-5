using Lab5.App.Models;

namespace Lab5.App.Services
{
    public interface IStudentService
    {
        void AddGrade(Student student, int grade);
        void PrintStudentReport(Student student); 
        void SearchAndDisplayStudent(Group group, string query);
    }
}