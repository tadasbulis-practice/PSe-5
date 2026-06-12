using System;

public class CashPaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} EUR in cash");
    }
}