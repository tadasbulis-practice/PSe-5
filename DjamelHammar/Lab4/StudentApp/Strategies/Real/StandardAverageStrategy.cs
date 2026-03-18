using System.Collections.Generic;
using StudentApp.Interfaces;

namespace StudentApp.Strategies.Real
{
    public class StandardAverageStrategy : IAverageStrategy
    {
        public int Calculate(List<int> grades)
        {
            if (grades.Count == 0) return 0;
            int sum = 0;
            foreach (var g in grades) sum += g;
            return sum / grades.Count;
        }
    }
}