using System.Collections.Generic;
using System.Linq;

namespace Nassim.Lab4.Nassim.Service
{
    public class SimpleAverageStrategy : IAverageStrategy
    {
        public double Calculate(List<Student> students)
            => students.Count == 0 ? 0 : students.Average(s => s.AverageGrade);
    }
}