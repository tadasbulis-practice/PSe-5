using System.Collections.Generic;
using Lab6.App.Models;

namespace Lab6.App.Interfaces
{
    public interface IStudentPrinter
    {
        void PrintStudents(IReadOnlyList<Student> students);
    }
}