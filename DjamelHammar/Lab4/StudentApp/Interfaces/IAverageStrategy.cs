using System.Collections.Generic;

namespace StudentApp.Interfaces
{
    public interface IAverageStrategy
    {
        int Calculate(List<int> grades);
    }
}