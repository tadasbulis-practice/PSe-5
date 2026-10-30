# OOP Lab 4

Name: Mohamed Faouzi  
Group: PSe5  

## Topic
Dependency Isolation using Fake and Stub.

## Description
This project extends the Lab 3 payment module by isolating the payment dependency through the IPaymentService interface.

The PaymentProcessor class does not create payment services directly. Instead, the dependency is passed through the constructor.

## Implemented
- IPaymentService interface
- Real implementation: CashPaymentService
- Fake implementation: FakePaymentService
- Stub implementation: StubPaymentService
- Constructor injection
- Two logical branches:
  - Successful payment
  - Failed payment

## Why this is useful
This architecture allows the business logic to work even if the real payment module is unfinished or changes later. Fake and Stub classes allow testing and development without depending on the real implementation.