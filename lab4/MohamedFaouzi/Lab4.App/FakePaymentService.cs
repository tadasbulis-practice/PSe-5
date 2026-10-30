using System;

public class FakePaymentService : IPaymentService
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"FAKE payment completed for {amount} EUR");
        return true;
    }
}