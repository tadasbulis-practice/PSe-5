using System;

public class StubPaymentService : IPaymentService
{
    private readonly bool _result;

    public StubPaymentService(bool result)
    {
        _result = result;
    }

    public bool Pay(decimal amount)
    {
        Console.WriteLine($"STUB payment result: {_result}");
        return _result;
    }
}