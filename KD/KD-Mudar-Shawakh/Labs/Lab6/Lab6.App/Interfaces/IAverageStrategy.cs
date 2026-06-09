using System.Collections.Generic;
using Lab6.App.Models;

namespace Lab6.App.Interfaces
{
    public interface IAverageStrategy
    {
        // LAB-5 used Calculate(Student) — one student at a time.
        // LAB-6 calculates across a whole collection (group average).
        double Calculate(IReadOnlyList<Student> students);
    }
}