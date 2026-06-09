"# OOP Lab 5" 
Full Name: Mudar Shawakh
Date: 13.03.2026
Task: 2


## LAB-5: Interface Patterns in Practice (Variant 2)

**Pattern Implemented:** Strategy Pattern
**Domain:** Average Calculation

**Explanation:**
The Strategy pattern was implemented to abstract the algorithm used to calculate a student's average grade. The `IStudentService` relies on the `IAverageStrategy` interface rather than a concrete calculation method.

**Why this architecture is beneficial:**
1. **Open/Closed Principle:** We can add new calculation methods (like `WeightedAverageStrategy` or `MedianAverageStrategy`) without modifying the `StudentService` class.
2. **Clean Constructor Injection:** The calculation strategy is passed into `StudentService` via its constructor, removing all business logic and instantiation from the service layer.
3. **Runtime Switching:** The system can dynamically change how averages are calculated at runtime based on user input or configuration, entirely decoupling the "WHAT" from the "HOW".


"# OOP Lab 4" 
Full Name: Mudar Shawakh
Date: 13.03.2026
Task: 2

## LAB-4: Dependency Isolation using Fake and Stub

**What was isolated and why?**
The searching mechanism (`IStudentFinder`) was isolated from the core business logic (`StudentService`). The dependency is now passed via Constructor Injection rather than using the `new` keyword inside the service class.

**Why this architecture is beneficial:**
1. **Independent Development:** Using `FakeStudentFinder` allows UI/Service development to continue even if the real database module is incomplete.
2. **Controlled Logic Branches:** `StubStudentFinder` allows us to inject exact test states (like `null`) to trigger specific error-handling branches without setting up a real database.
3. **Runtime Flexibility:** The application can switch implementations at runtime without modifying the business logic.



"# OOP Lab 3" 
Full Name: Mudar Shawakh
Date: 26.02.2026
Task: 2

Architectural Explanation

In this lab, I used the Strategy Design Pattern to calculate student averages. The main goal was to use Abstraction and Polymorphism to make the code flexible.

Model (Student.cs): This class just holds the student's info. I took out the calculation math to give it just one job to do. The Grades list is left open so the outside strategy classes can read the data.

Interfaces (IAverageStrategy.cs & IStudentService.cs): These basically set the rules. IAverageStrategy makes sure any grading system we create has a Calculate method, meaning the rest of the app doesn't have to depend on specific classes.

The Strategies (SimpleAverageStrategy & DropLowestAverageStrategy): These are two totally different ways to calculate the average. I set them up without changing the main student model or relying on messy if/else blocks.

Service (StudentService.cs): This acts like the middleman. It handles adding the grades and printing out the final report using whichever calculation strategy we pass to it.

Program (Program.cs): The main file is super clean now. There is zero business logic in here—it just uses the interfaces to run the app and show off polymorphism in action.


