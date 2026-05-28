📘 LAB‑5 — Interface Patterns in Practice
This lab extends the dependency‑isolated architecture from LAB‑4 by applying real interface‑based design patterns used in modern software systems.
The goal is to demonstrate Strategy, Repository, Service Abstraction, and Adapter patterns — all implemented through interfaces and constructor injection.

The system allows runtime switching between implementations without modifying business logic.

🎯 1. Architecture Overview
The final architecture follows this dependency flow:

Code
Program → IMenuService → StudentService
StudentService → IStudentRepository
StudentService → IStudentPrinter
StudentService → IStudentValidator
StudentService → IAverageStrategy
Key principles:

Program.cs uses only interfaces

No business logic in Main

All dependencies injected via constructors

Multiple implementations per interface

Runtime behavior switching using ServiceFactory

🟦 2. Strategy Pattern — Student Printing
Interface:
Code
IStudentPrinter
Implementations:
ConsoleStudentPrinter — prints student info to the console

FileStudentPrinter — writes student info to a file

JsonStudentPrinter — outputs student info in JSON format

Why this is Strategy:
The printing behavior can be changed at runtime without modifying StudentService.
Only the injected implementation changes.

🟩 3. Strategy Pattern — Average Calculation
Interface:
Code
IAverageStrategy
Implementations:
SimpleAverageStrategy — standard arithmetic mean

WeightedAverageStrategy — applies weights to grades

MedianAverageStrategy — calculates the median

Why this is Strategy:
The student’s average calculation is fully isolated.
Student.GetAverage() does not know how the average is computed — it simply delegates to the injected strategy.

🟧 4. Service Abstraction — Menu System
Interface:
Code
IMenuService
Implementations:
ConsoleMenuService

DebugMenuService

WebMenuSimulationService

(Optional) AlternativeMenuService

Why this is Service Abstraction:
The user interface layer is completely separated from business logic.
The system can switch between console, debug, or web‑simulation modes without changing any business logic.

🟥 5. Repository Pattern — Student Data Access
Interface:
Code
IStudentRepository
Implementations:
MemoryStudentRepository — stores students in memory

FileStudentRepository — loads/saves students from a file

ApiStudentRepository — simulates remote API access

Why this is Repository:
The repository abstracts where student data comes from.
StudentService does not know whether data is from memory, file, or API — it only uses the interface.

🟪 6. Adapter Pattern — Validation
Interface:
Code
IStudentValidator
Components:
LegacyValidator — old validation system (incompatible interface)

StudentValidatorAdapter — adapts legacy validator to IStudentValidator

Why this is Adapter:
The adapter translates the legacy validation API into the modern interface.
This allows the system to use old code without modifying the business logic.

🏆 7. Runtime Behavior Switching
The program supports runtime selection of implementations using command‑line arguments:

Code
dotnet run console
dotnet run debug
dotnet run web
dotnet run test
dotnet run file-repo
dotnet run api-repo
ServiceFactory constructs the correct dependency graph based on the selected mode.

Why this matters:
No business logic changes

No code duplication

Easy to extend

Perfect demonstration of interface‑driven architecture

🧠 8. Why This Architecture Is Beneficial
Testability — Fake and Stub implementations allow isolated testing

Flexibility — Behavior can be swapped at runtime

Maintainability — Clear separation of concerns

Scalability — New strategies, repositories, or adapters can be added without modifying existing code

Real‑world relevance — These patterns are used in enterprise systems (ASP.NET, microservices, DDD)

📦 9. Summary of Patterns Implemented
Pattern	Interface	Implementations	Purpose
Strategy	IStudentPrinter	Console, File, JSON	Switch printing behavior
Strategy	IAverageStrategy	Simple, Weighted, Median	Switch grade calculation
Service Abstraction	IMenuService	Console, Debug, Web	UI layer isolation
Repository	IStudentRepository	Memory, File, API	Data access abstraction
Adapter	IStudentValidator	Adapter + Legacy	Integrate legacy validation


🚀 10. Conclusion
This lab demonstrates a complete, interface‑driven architecture using multiple design patterns.
The system is modular, testable, and easily extendable — matching real‑world software engineering practices.