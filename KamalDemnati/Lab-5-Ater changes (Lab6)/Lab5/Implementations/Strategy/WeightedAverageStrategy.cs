using Lab5.Interfaces;
using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Implementations.Strategy
{
    public class WeightedAverageStrategy : IAverageStrategy
    {
        public double Calculate(List<Student> students)
        {
            if (!students.Any()) return 0;

            double total = students.Sum(s => s.Grade * s.Weight);
            double weightSum = students.Sum(s => s.Weight);

            return weightSum == 0 ? 0 : total / weightSum;
        }
    }
}
