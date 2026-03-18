In this lab,

all dependencies of StudentService were isolated using interfaces and constructor injection, allowing the service to work with different implementations without changing its code. 

Real classes provide the actual logic, Fake classes return stable, predictable results, and Stub classes allow controllable outputs to test different scenarios. 

This setup demonstrates how dependency isolation improves modularity, testability, and flexibility, and shows two logical branches for each strategy to simulate different business situations.