using System;

public class App
{
    public void Run()
    {
        Console.WriteLine("Scenario 1: Fake payment");
        IPaymentService fakePayment = new FakePaymentService();
        PaymentProcessor fakeProcessor = new PaymentProcessor(fakePayment);
        fakeProcessor.Process(100m);

        Console.WriteLine();

        Console.WriteLine("Scenario 2: Stub successful payment");
        IPaymentService successStub = new StubPaymentService(true);
        PaymentProcessor successProcessor = new PaymentProcessor(successStub);
        successProcessor.Process(150m);

        Console.WriteLine();

        Console.WriteLine("Scenario 3: Stub failed payment");
        IPaymentService failedStub = new StubPaymentService(false);
        PaymentProcessor failedProcessor = new PaymentProcessor(failedStub);
        failedProcessor.Process(200m);
    }
}