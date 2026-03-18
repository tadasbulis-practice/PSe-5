using System.Collections.Generic;
using StudentApp.Interfaces;

namespace StudentApp.Strategies.Fake
{
    public class FakeAverageStrategy : IAverageStrategy
    {
        public int Calculate(List<int> grades) => 42; // always returns fake value
    }
}