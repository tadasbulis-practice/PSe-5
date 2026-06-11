using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public interface IAverageStrategy
    {
        double Calculate(List<Student> students);
    }
}