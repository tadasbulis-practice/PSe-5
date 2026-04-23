# LAB-7 – Generic Repository in Object-Oriented Architecture

## Goal
This lab extends LAB-6 by introducing a generic repository and a shared entity interface.

## Main changes from LAB-6
- Added `IEntity`
- Added generic `IRepository<T>`
- Replaced old student-specific repositories with `MemoryRepository<T>`
- Kept service architecture based on interfaces
- Used LINQ where applicable

## Architecture
Program -> IMenuService -> StudentService

StudentService depends on:
- IRepository<Student>
- IStudentPrinter
- IStudentValidator
- IAverageStrategy

## Generic repository
The repository is now reusable for any model implementing `IEntity`.

Examples:
- Student : IEntity
- Order : IEntity
- Teacher : IEntity

## LINQ usage
LINQ is used in:
- validation
- average calculations
- repository queries
- collection transformations

## Design principles used
- encapsulation
- separation of concerns
- dependency injection
- generic programming
- reusable architecture
