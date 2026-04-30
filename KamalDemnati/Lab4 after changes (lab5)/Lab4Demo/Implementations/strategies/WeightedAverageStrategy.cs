using Lab4Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations.strategies
{
    public class WeightedAverageStrategy : IAverageStrategy
    {
        public double CalculateAverage(List<double> grades)
        {
            double total = 0;
            double weight = 1;
            double weightSum = 0;

            foreach (var g in grades)
            {
                total += g * weight;
                weightSum += weight;
                weight++;
            }

            return total / weightSum;
        }
    }
}
