using System;

public class PaymentProcessor
{
    private readonly IPaymentService _paymentService;

    public PaymentProcessor(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public void Process(decimal amount)
    {
        bool success = _paymentService.Pay(amount);

        if (success)
        {
            Console.WriteLine("Order completed successfully.");
        }
        else
        {
            Console.WriteLine("Payment failed. Order was not completed.");
        }
    }
}