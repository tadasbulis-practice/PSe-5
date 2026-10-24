# OOP Lab 5

Name: Mohamed Faouzi  
Group: Group 1  

## Lab topic
Interface Patterns in Practice.

## Implemented patterns

### Strategy Pattern
Used with:
- IStudentPrinter
- IAverageStrategy

Different implementations can be selected at runtime:
- ConsoleStudentPrinter
- FileStudentPrinter
- JsonStudentPrinter
- SimpleAverageStrategy
- WeightedAverageStrategy
- MedianAverageStrategy

### Repository Pattern
Used with:
- IStudentRepository

Implementations:
- MemoryStudentRepository
- FileStudentRepository
- ApiStudentRepository

This isolates data access from StudentService.

### Adapter Pattern
Used with:
- StudentValidatorAdapter

The adapter connects LegacyStudentValidation to IStudentValidator.

## Architecture explanation
StudentService depends only on interfaces. It does not create concrete classes directly.

Dependencies are passed using constructor injection.

Program.cs is the composition root where implementations are selected.

This makes the system flexible and easy to extend.