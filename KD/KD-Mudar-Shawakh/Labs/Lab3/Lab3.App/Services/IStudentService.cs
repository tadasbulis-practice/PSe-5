using Lab3.App.Models;
using Lab3.App.Strategies;

namespace Lab3.App.Services
{
    public interface IStudentService
    {
        void AddGrade(Student student, int grade);
        void PrintStudentReport(Student student, IAverageStrategy strategy);
    }
}