using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1.Interfaces;
using CW1.Models;

namespace CW1.Services
{
    public class AverageCalculator
    {
        public double Calculate(Student student)
        {
            if (student.Grades.Count == 0)
            {
                return 0;
            }

            double sum = 0;

            foreach (int grade in student.Grades)
            {
                sum += grade;
            }

            return sum / student.Grades.Count;
        }
    }
}
