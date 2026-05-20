Overview
In LAB‑5, I extended the architecture from LAB‑4 by implementing real software design patterns using interfaces.
The goal was to build a flexible, modular system where behavior can be changed at runtime without modifying business logic.

The project follows these rules:

Program.cs uses only interfaces

No business logic is inside Main

All dependencies are injected through constructors

Each interface has multiple implementations

Runtime switching is demonstrated

StudentService depends only on interfaces, never concrete classes

1. Strategy Pattern
1.1 IStudentPrinter (Printing Strategy)
This interface defines how students are printed.

I implemented three strategies:

ConsoleStudentPrinter – prints to console

FileStudentPrinter – writes student list to a text file

JsonStudentPrinter – saves students in JSON format

Why Strategy Pattern?  
Because the system can switch printing behavior at runtime without changing StudentService.

Example runtime switching in Program.cs:

csharp
IStudentPrinter printer = new JsonStudentPrinter();
// or new FileStudentPrinter();
// or new ConsoleStudentPrinter();
1.2 IAverageStrategy (Average Calculation Strategy)
This interface defines how a student’s average grade is calculated.

I implemented:

SimpleAverageStrategy

WeightedAverageStrategy

MedianAverageStrategy

Why Strategy Pattern?  
Different algorithms can be swapped freely without modifying StudentService.

2. Repository Pattern
IStudentRepository
This interface abstracts where student data comes from.

I implemented:

MemoryStudentRepository – in‑memory data

FileStudentRepository – loads students from a file

ApiStudentRepository – simulated API source

Why Repository Pattern?  
StudentService does not know or care whether data comes from memory, file, or API.

Example runtime switching:

csharp
IStudentRepository repository = new ApiStudentRepository();
// or new FileStudentRepository();
// or new MemoryStudentRepository();
3. Service Abstraction Pattern
IMenuService
This interface abstracts how the user interacts with the system.

I implemented:

ConsoleMenuService

DebugMenuService

WebMenuSimulationService

Why Service Abstraction?  
The UI layer is separated from business logic.
StudentService never interacts with the console directly.

4. Adapter Pattern
IStudentValidator
The project includes a legacy validation system:

Code
LegacyStudentValidation
Its interface is incompatible with the new architecture.

I created:

StudentValidatorAdapter

This adapter converts the legacy method into the IStudentValidator interface.

Why Adapter Pattern?  
It allows using old code inside a modern interface‑based architecture.

5. Runtime Switching (Required)
In Program.cs, I demonstrated switching implementations without modifying StudentService:

csharp
IStudentPrinter printer = new JsonStudentPrinter();
IAverageStrategy strategy = new WeightedAverageStrategy();
IStudentRepository repository = new ApiStudentRepository();
IStudentValidator validator = new StudentValidatorAdapter();

StudentService service = new StudentService(repository, printer, strategy, validator);

IMenuService menu = new WebMenuSimulationService(service);
menu.Run();
This proves the architecture is flexible and loosely coupled.

6. Architecture Summary
Code
Program.cs
    ↓
IMenuService
    ↓
StudentService
    ↓ ↓ ↓ ↓
IStudentRepository
IStudentPrinter
IAverageStrategy
IStudentValidator
Each interface has multiple implementations, and StudentService depends only on the interfaces.

7. What I Learned
How to apply Strategy, Repository, Service, and Adapter patterns

How to isolate dependencies using interfaces

How to use constructor injection

How to design flexible, testable architecture

How to switch behavior at runtime without touching business logic