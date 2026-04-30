using Lab4Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Demo.Implementations.strategies
{
    public class MedianAverageStrategy : IAverageStrategy
    {
        public double CalculateAverage(List<double> grades)
        {
            var sorted = grades.OrderBy(x => x).ToList();
            int mid = sorted.Count / 2;

            return sorted.Count % 2 == 0
                ? (sorted[mid - 1] + sorted[mid]) / 2
                : sorted[mid];
        }
    }
}
