using Lab4Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations.strategies
{
    public class SimpleAverageStrategy : IAverageStrategy
    {
        public double CalculateAverage(List<double> grades)
            => grades.Average();
    }

}
