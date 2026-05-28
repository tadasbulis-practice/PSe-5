using JohnBastille.Lab_4.Interfaces;

namespace Lab_4.Services;

/// <summary>
/// Stub implementation of IAverageStrategy for testing.
/// Always returns a fixed average value.
/// </summary>
public class StubAverageStrategy : IAverageStrategy
{
    private readonly double _fixedAverage;

    public StubAverageStrategy(double fixedAverage)
    {
        _fixedAverage = fixedAverage;
    }

    public double CalculateAverage(List<int> grades)
    {
        return _fixedAverage;
    }
}