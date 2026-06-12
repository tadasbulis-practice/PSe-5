using System;

public class CardPaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} EUR by card");
    }
}