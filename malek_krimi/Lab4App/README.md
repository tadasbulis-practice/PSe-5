# LAB-4: Implementing Isolation in Practice

## Goal
The goal of this lab is to apply isolation in object-oriented programming by separating responsibilities into independent interfaces and implementations.

## What isolation means in this project
Isolation means that each part of the system has its own responsibility and can be changed independently without affecting the rest of the application.

Examples:
- menu logic is isolated in `IMenuService`
- student printing is isolated in `IStudentPrinter`
- average calculation is isolated in `IAverageStrategy`
- searching is isolated in `IStudentFinder`
- validation is isolated in `IStudentValidator`

## Architecture
`Program.cs` works only with interfaces.

Main structure:
- `Program`
- `Student`
- Interfaces
- Implementations

## Isolation examples
- Changing from `SimpleMenu` to `FancyMenu` does not change program logic
- Changing from `ConsoleStudentPrinter` to `DetailedStudentPrinter` only affects output
- Changing from `SimpleAverage` to `WeightedAverage` only affects calculation
- Changing from `LinearSearch` to `CaseInsensitiveSearch` only affects search behavior
- Changing from `BasicValidator` to `StrictValidator` only affects validation rules

## Benefits
- clean architecture
- easy maintenance
- easy extension
- loose coupling
- better readability

## Conclusion
The application demonstrates isolation by separating responsibilities into interfaces and implementations. Each component works independently and can be replaced without changing the overall business logic.
