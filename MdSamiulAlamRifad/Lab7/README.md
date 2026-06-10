# Lab-7 — Containers in Object-Oriented Architecture (C#)

## What this lab does

Extends the Lab-5 architecture to work with **collections of objects** instead of single objects.  
All patterns from Lab-5 (Repository, Strategy, Printer, Validator, Adapter, Menu) are updated to support `List<Student>`.

---

## Architecture

```
Program.cs (Composition Root)
    │
    └── IMenuService → ConsoleMenuService
            │
            └── StudentService
                    ├── IStudentRepository  → MemoryStudentRepository
                    ├── IStudentPrinter     → ConsoleStudentPrinter / FileStudentPrinter / JsonStudentPrinter
                    ├── IAverageStrategy    → SimpleAverageStrategy / WeightedAverageStrategy / MedianAverageStrategy
                    └── IStudentValidator   → StudentValidatorAdapter (wraps LegacyStudentValidation)
```

---

## How containers are used

- `MemoryStudentRepository` holds a **private** `List<Student> _students` — never exposed directly
- All access goes through methods: `GetAll()`, `GetById()`, `Add()`, `Remove()`
- `GetAll()` returns a **copy** of the list so callers cannot mutate internal state
- `ValidateAll()` filters a list and returns only valid students
- `Calculate()` takes a list and returns a group average
- `PrintStudents()` takes a list and displays all of them

---

## Design decisions

| Decision | Reason |
|---|---|
| `List<Student>` is private in repository | Encapsulation — nothing outside can break internal state |
| `GetAll()` returns a copy, not the real list | Prevents accidental mutation from outside |
| `ValidateAll()` in validator, not service | Single responsibility — validator owns validation logic |
| Three average strategies | Open/Closed principle — swap strategies without changing service |
| Three printer implementations | Flexible output — console, file, or JSON without changing service |
| `Program.cs` only wires dependencies | Clean composition root — zero business logic |

---

## Tasks completed

- **Task 1** — Repository with container (`GetAll`, `GetById`, `Add`, `Remove`)
- **Task 2** — Printer for collections (`ConsoleStudentPrinter`, `FileStudentPrinter`, `JsonStudentPrinter`)
- **Task 3** — Strategy for collections (`SimpleAverageStrategy`, `WeightedAverageStrategy`, `MedianAverageStrategy`)
- **Task 4** — Validation for collections (`Validate` single + `ValidateAll` list)
- **Task 5** — Service logic with containers (`PrintAllStudents`, `CalculateGroupAverage`, filter, sort)
- **Task 6** — Full flow scenario (Add → Retrieve → Validate → Calculate → Print)

---

## How to run

```bash
cd Lab7
dotnet run
```

---

## Optional extensions implemented

- `FilterByMinAverage(double min)` — filter students by minimum average grade
- `GetStudentsSortedByAverage()` — sort students best-to-worst
- `FileStudentPrinter` — export to `students.txt`
- `JsonStudentPrinter` — export to `students.json`
