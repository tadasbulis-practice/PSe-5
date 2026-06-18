public class FakeAverageStrategy : IAverageStrategy
{
    public double Calculate(Student student)
    {
        return 8.0;
    }
}