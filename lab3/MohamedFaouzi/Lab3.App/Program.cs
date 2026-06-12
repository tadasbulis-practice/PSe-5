class Program
{
    static void Main(string[] args)
    {
        IPaymentService payment = new CashPaymentService();

        App app = new App(payment);
        app.Run();
    }
}