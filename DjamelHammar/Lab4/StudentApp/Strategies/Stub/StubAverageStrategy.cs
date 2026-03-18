using System.Collections.Generic;
using StudentApp.Interfaces;

namespace StudentApp.Strategies.Stub
{
    public class StubAverageStrategy : IAverageStrategy
    {
        public int Result { get; set; } = 0; // configurable result
        public int Calculate(List<int> grades) => Result;
    }
}