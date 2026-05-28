# LAB-5 – Interface patterns praktikoje

## Goal
The goal of this lab is to apply interfaces in practice using:
- Strategy pattern
- Repository pattern
- Service pattern
- Adapter pattern

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

## Runtime switching
The application supports runtime switching by changing implementations in Program.cs.

Example:
- printer can be ConsoleStudentPrinter, FileStudentPrinter, or JsonStudentPrinter
- repository can be MemoryStudentRepository, FileStudentRepository, or ApiStudentRepository
- average strategy can be SimpleAverage, WeightedAverage, or MedianAverage

## Dependency injection
StudentService uses constructor injection to receive all dependencies via interfaces.

## Checklist
- Program.cs uses only interfaces ✔
- At least 2 implementations ✔
- Runtime switching works ✔
- Constructor injection used ✔
- README explains architecture ✔