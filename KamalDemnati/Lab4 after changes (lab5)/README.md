Overview

In this lab, I extended the architecture from LAB-4 by applying design patterns using interfaces. The goal was to make the system flexible and allow changing behavior without modifying the main business logic.

All components depend on interfaces, and implementations can be switched at runtime.

Implemented Patterns
1. Strategy Pattern

Used for:

Student printing (IStudentPrinter)
Average calculation (IAverageStrategy)

Implementations:

Printing: Console, File
Average: Simple, Weighted, Median


2. Repository Pattern

Used for data access (IStudentRepository)

Implementations:

MemoryStudentRepository
FileStudentRepository

This separates data access from business logic. StudentService does not depend on how or where the data is stored.

3. Service Abstraction

Used for menu handling (IMenuService)

Implementations:

ConsoleMenuService
DebugMenuService

This separates user interaction from business logic and allows changing the interface without affecting core functionality.

- Final Architecture

Program -> IMenuService -> StudentService
StudentService -> IStudentRepository
StudentService -> IStudentPrinter
StudentService -> IAverageStrategy

All dependencies are injected through constructors.

- Runtime Flexibility

The program allows switching implementations at runtime. For example:

Changing average calculation strategy
Switching between console and file printing
Using different data sources

This is done without modifying StudentService, only by changing dependencies in Program.cs.
