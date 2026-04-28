Overview

In this lab, I extended the architecture from LAB-5 by introducing containers (collections of objects) into the system.

The goal was to make the system capable of working with multiple students instead of a single object, while maintaining clean architecture and design principles.

All components were updated to operate on List<Student>, and the system now supports processing, validating, and displaying groups of data.

Container Usage

In this lab, containers are implemented using:

List<Student>

The container is used inside the repository:

Stores all students
Provides access through controlled methods

All system components now work with collections:

Repository -> returns multiple students
Validator -> validates collections
Strategy -> calculates group averages
Printer -> prints multiple students

The container is private and encapsulated, and can only be accessed via repository methods.

Implemented / Extended Patterns
1. Strategy Pattern (Extended)

Used for:

Average calculation (IAverageStrategy)

Implementations:

SimpleAverageStrategy
WeightedAverageStrategy
MedianAverageStrategy

Extension:

In LAB-5 → worked with single object
In LAB-6 → works with List<Student>
2. Repository Pattern (Extended)

Used for:

Data access (IStudentRepository)

Implementation:

MemoryStudentRepository

Extension:

Now manages a container (List<Student>)
Supports:
GetAll()
Add()
Remove()
GetById()

This centralizes all collection logic in one place.

3. Printer Strategy (Extended)

Used for:

Output (IStudentPrinter)

Implementations:

ConsoleStudentPrinter
FileStudentPrinter
JsonStudentPrinter

Extension:

Now prints multiple students instead of one
4. Validator Pattern (Extended)

Used for:

Validation (IStudentValidator)

Extension:

Supports:
Single validation → Validate(Student)
Collection validation → ValidateAll(List<Student>)
5. Adapter Pattern

Used for:

Integrating legacy validator

Implementation:

StudentValidatorAdapter

Allows old validation logic to work with the new interface-based system.

6. Service Abstraction

Used for:

Menu handling (IMenuService)

Implementation:

ConsoleMenuService

Separates user interaction from business logic.

Final Architecture

Program → IMenuService → StudentService

StudentService depends on:

IStudentRepository
IStudentPrinter
IAverageStrategy
IStudentValidator

All dependencies are injected through constructors.

All components now operate on collections instead of single objects.

Runtime Flexibility

The program allows switching implementations at runtime.

Examples:

Changing average calculation strategy
Switching between console, file, or JSON printing
Changing validation logic
Changing data storage implementation

This is done without modifying StudentService, only by changing dependencies in Program.cs.

Design Decisions
Containers are encapsulated inside the repository
No direct access to List<Student> from outside
Business logic is placed in StudentService, not in Program.cs
Interfaces are used for all major components
Constructor injection is used for flexibility
Each class has a single responsibility