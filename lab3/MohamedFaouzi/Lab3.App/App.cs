public class App
{
    private readonly IPaymentService _paymentService;

    public App(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public void Run()
    {
        _paymentService.Pay(100m);
    }
}