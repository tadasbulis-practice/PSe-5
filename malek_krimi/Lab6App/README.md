# LAB-6 – Containers in Object-Oriented Architecture (C#)

## Goal
This project extends the LAB-5 architecture to support working with multiple objects using containers.
The main container used in the project is `List<Student>`.

## Architecture
Program -> IMenuService -> StudentService

StudentService depends on:
- IStudentRepository
- IStudentPrinter
- IStudentValidator
- IAverageStrategy

## How containers are used
Containers are used to manage collections of students instead of single student objects.
The repository stores students in a private `List<Student>` and exposes access only through methods.
This keeps encapsulation and prevents direct external modification.

## Updated interfaces
### IStudentRepository
- `GetAll()`
- `GetById(int id)`
- `Add(Student student)`
- `Remove(int id)`

### IStudentPrinter
- `PrintStudents(List<Student> students)`

### IAverageStrategy
- `Calculate(List<Student> students)`

### IStudentValidator
- `Validate(Student student)`
- `ValidateAll(List<Student> students)`

## Patterns extended from LAB-5
### Strategy pattern
- `ConsoleStudentPrinter`
- `FileStudentPrinter`
- `JsonStudentPrinter`
- `SimpleAverage`
- `WeightedAverage`
- `MedianAverage`

### Repository pattern
- `MemoryStudentRepository`
- `FileStudentRepository`
- `ApiStudentRepository`

### Service pattern
- `StudentService`
- `ConsoleMenuService`
- `DebugMenuService`
- `WebMenuSimulationService`

### Adapter pattern
- `StudentValidatorAdapter` adapts `LegacyStudentValidationSystem` to `IStudentValidator`

## Design decisions
- The student container is private inside repository implementations.
- Program.cs uses only interfaces and stays clean.
- Business logic is inside `StudentService`.
- Constructor injection is used.
- Validation, printing, and average calculation all work with collections.

## Full flow scenario
The program:
1. Adds multiple students
2. Retrieves all students through the repository
3. Validates students
4. Calculates group average
5. Prints results

## LAB-6 checklist
- Program uses only interfaces ✔
- Constructor injection used ✔
- No business logic in Program.cs ✔
- At least one `List<T>` used ✔
- Container is private ✔
- No direct external access ✔
- Repository supports GetAll/Add/Remove ✔
- Strategy works with collections ✔
- Printer works with collections ✔
- Validator works with collections ✔
