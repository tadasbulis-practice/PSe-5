# CW-1 — OOP C# (Refactored Solution)

**Author:** Mudar Shawakh
**Date:** 2026-05-21

## Overview

This is the refactored version of the CW-1 starter project. The original
`Program.cs` (a single ~250-line "God class") has been split into clean OOP
layers — Models / Interfaces / Services / UI — with constructor injection and
one shared interface (`IStudentRepository`).

> Note: the project targets **net10.0** (the original starter specified
> net8.0). The architecture, LINQ logic, and behaviour are identical on either
> target framework.

## Tasks completed

| Task | Description | Status |
|------|-------------|--------|
| Task 1 | Refactor into Models / Interfaces / Services / UI layers + interface + DI | Done |
| Task 2 | Menu items 7, 8, 9 in TWO versions — with LINQ and without LINQ | Done |
| Task 3 | `StubStudentRepository` + `--stub` command-line switch | Done |
| Drills | All 5 `_Plain` methods in `LINQ_Drills.cs` | Done |

## Project structure

```
CW1After/
├── Program.cs                 <- composition root (parses args, wires everything)
├── DrillTests.cs              <- optional PASS/FAIL self-test for the drills
├── LINQ_Drills.cs             <- 5 drills (LINQ + no-LINQ versions)
├── Models/
│   ├── Student.cs
│   └── Group.cs
├── Interfaces/
│   └── IStudentRepository.cs
├── Services/
│   ├── MemoryStudentRepository.cs    <- real data (5 students)
│   ├── StubStudentRepository.cs      <- Task 3 (1 test student)
│   ├── StudentValidator.cs
│   ├── AverageCalculator.cs          <- DRY: average formula lives here only
│   ├── StudentService.cs
│   └── ReportService.cs              <- Task 2 (LINQ + no-LINQ reports)
└── UI/
    └── ConsoleMenu.cs                <- only place that uses Console
```

## Design notes

- **SRP** — each class has one job (validation, averaging, reporting, etc.).
- **Constructor injection** — every service receives its dependencies through
  its constructor. The only place that knows about concrete repository classes
  is `Program.cs`.
- **Abstraction** — `StudentService`, `StudentValidator` and `ReportService`
  depend on `IStudentRepository`, not on a concrete class. This is what makes
  the `--stub` switch a single line.
- **DRY** — the average formula exists once, in `AverageCalculator`.
- **UI separation** — `Console.WriteLine` / `Console.ReadLine` appear only
  inside `UI/ConsoleMenu.cs`.
- **No global static state** — the student/group lists are private fields
  inside the repositories, not a static list in `Program`.

### Task 2 — no-LINQ versions

For menu items 7, 8 and 9, `ReportService` provides both a LINQ method and a
`...WithoutLinq` twin that produces identical output using only
`for` / `foreach` / `if` / `List.Add` / `List.Sort`. Running each item prints
both blocks so the outputs can be compared side by side.

## How to run

From the solution folder (`CW-1-after`):

```bash
# Normal mode — the 5 students from MemoryStudentRepository
dotnet run --project CW1After

# Stub mode — only "Test Student" (Id 999) in group TEST
dotnet run --project CW1After -- --stub

# Run the LINQ drill self-test (prints PASS/FAIL for all 5 drills)
dotnet run --project CW1After -- --drills
```

### Where to see the results

- **Task 1:** menu items 1–6 (list, add, add grade, average, find, validate).
- **Task 2:** menu items 7, 8, 9 — each shows the LINQ and no-LINQ output.
- **Task 3:** the startup line reports which repository is active; run with
  `--stub` and choose menu item 1 to see the single test student.
- **Drills:** run with `--drills` for the PASS/FAIL self-test.