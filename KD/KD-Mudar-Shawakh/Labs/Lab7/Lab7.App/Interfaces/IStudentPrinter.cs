using System.Collections.Generic;
using Lab7.App.Models;

namespace Lab7.App.Interfaces
{
    public interface IStudentPrinter
    {
        void PrintStudents(IReadOnlyList<Student> students);
    }
}