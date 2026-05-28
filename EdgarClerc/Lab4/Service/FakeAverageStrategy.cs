using lab4.model;

namespace Lab4.Service;

public class FakeAverageStrategy : IAverageStrategy
{
    public double Average(StudentProfile student)
    {
        return 5.26;
    }
}
