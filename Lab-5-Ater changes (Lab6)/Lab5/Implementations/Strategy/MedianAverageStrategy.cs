using Lab5.Interfaces;
using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Implementations.Strategy
{
    public class MedianAverageStrategy : IAverageStrategy
    {
        public double Calculate(List<Student> students)
        {
            if (!students.Any()) return 0;

            var ordered = students.Select(s => s.Grade).OrderBy(g => g).ToList();
            int count = ordered.Count;

            if (count % 2 == 1)
                return ordered[count / 2];

            return (ordered[count / 2 - 1] + ordered[count / 2]) / 2.0;
        }
    }
}
