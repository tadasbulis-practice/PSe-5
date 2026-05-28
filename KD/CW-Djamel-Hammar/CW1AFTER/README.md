# CW1AFTER

## Student
Djamel Hammar

## Date
2026-05-28

---

# Completed Tasks

## Task 1
- Refactored the project into layers:
  - Models
  - Services
  - UI
  - Interfaces
- Added constructor injection
- Implemented `IStudentRepository`
- Separated business logic from UI logic

## Task 2
Implemented both LINQ and non-LINQ versions for menu items both side by side:

- 7) Top 3 by average
- 8) Students in group
- 9) Statistics

For each menu item, the application prints:

- LINQ implementation
- WITHOUT LINQ implementation

This demonstrates that both implementations produce the same results.

## Task 3


Implemented runtime dependency switching using command-line arguments.

Features:

* `MemoryStudentRepository` (default mode)
* `StubStudentRepository` (`--stub` mode)

The repository implementation is selected at runtime through `Program.cs` using the `IStudentRepository` interface and constructor injection.

Supported commands:

* `dotnet run`
* `dotnet run -- --stub`
* `dotnet run -- --linq-only`
* `dotnet run -- --nolinq-only`

Additional runtime flags were added to demonstrate LINQ and no-LINQ implementations separately.


---


# Run Modes

## Default mode

Run the application with the default memory repository:

```bash
dotnet run
````

Features:

* Uses `MemoryStudentRepository`
* Shows both:

  * LINQ implementations
  * WITHOUT LINQ implementations

---

## Stub repository mode

Run the application with the stub repository:

```bash
dotnet run -- --stub
```

Features:

* Uses `StubStudentRepository`
* Loads test data only
* Shows both:

  * LINQ implementations
  * WITHOUT LINQ implementations

---

## LINQ-only mode

Run the application showing only LINQ implementations:

```bash
dotnet run -- --linq-only
```

Features:

* Uses default memory repository
* Displays only LINQ versions for menu items:

  * 7) Top 3 by average
  * 8. Students in group
  * 9. Statistics

---

## no-LINQ-only mode

Run the application showing only non-LINQ implementations:

```bash
dotnet run -- --nolinq-only
```

Features:

* Uses default memory repository
* Displays only WITHOUT LINQ versions for menu items:

  * 7) Top 3 by average
  * 8. Students in group
  * 9. Statistics

---

## Combined modes

Flags can be combined.

Example:

```bash
dotnet run -- --stub --linq-only
```

or

```bash
dotnet run -- --stub --nolinq-only
```

```
```


# Project Structure

```text
CW1AFTER/
├── Interfaces/
├── Models/
├── Services/
├── UI/
├── Program.cs
├── README.md
└── CW1After.csproj

