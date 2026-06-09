using Lab4.App.Models;
using Lab4.App.Strategies;

namespace Lab4.App.Services
{
    public interface IStudentService
    {
        void AddGrade(Student student, int grade);
        void PrintStudentReport(Student student, IAverageStrategy strategy);
        void SearchAndDisplayStudent(Group group, string query); 
    }
}