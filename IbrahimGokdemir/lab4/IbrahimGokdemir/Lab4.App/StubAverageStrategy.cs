public class StubAverageStrategy : IAverageStrategy
{
    private readonly double _result;

    public StubAverageStrategy(double result)
    {
        _result = result;
    }

    public double Calculate(Student student)
    {
        return _result;
    }
}