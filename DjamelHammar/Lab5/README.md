# LAB-5 – Interface patterns praktikoje

OOP Laboratory Work Collection

Student: Djamel Hammar
Group: Pse-05
Course: Object-Oriented Programming (C#)

⸻


The system is designed so that behavior can be changed without modifying business logic.

## Architecture

Program.cs works only with interfaces and creates dependencies through constructor injection.

### Main flow
Program -> IMenuService -> StudentService

StudentService depends on:
- IStudentRepository
- IStudentPrinter
- IStudentValidator
- IAverageStrategy

## Patterns used

### 1. Strategy pattern
Used for:
- student printing
- average calculation

Implementations:
- IStudentPrinter:
  - ConsoleStudentPrinter
  - FileStudentPrinter
  - JsonStudentPrinter

- IAverageStrategy:
  - SimpleAverage
  - WeightedAverage
  - MedianAverage

### 2. Service pattern
Used for menu abstraction.

Implementations:
- ConsoleMenuService
- DebugMenuService
- WebMenuSimulationService

### 3. Repository pattern
Used for student storage abstraction.

Implementations:
- MemoryStudentRepository
- FileStudentRepository
- ApiStudentRepository

### 4. Adapter pattern
Used for validation.

LegacyStudentValidationSystem is adapted through StudentValidatorAdapter to work with IStudentValidator.



## Dependency injection
StudentService uses constructor injection to receive all dependencies via interfaces.
