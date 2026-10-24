using System;

public class CashPaymentService : IPaymentService
{
    public bool Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} EUR in cash");
        return true;
    }
}