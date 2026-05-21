# CW-1 — OOP C# Control Work

**Author:** [Md Samiul Alam Rifad]  
**Date:** [21 May,2026]

---

## Completed Tasks

| Task | Status |
|------|--------|
| Task 1 — Refactor into layers | ✅ Done |
| Task 2 — LINQ → loops        | ✅ Done |
| Task 3 — Runtime args + Stub | ✅ Done |
| Drills — 5 LINQ exercises    | ✅ Done |

---

## Project Structure

```
CW1After/
├── Program.cs                  ← composition root, parses --stub arg
├── Models/
│   ├── Student.cs
│   └── Group.cs
├── Interfaces/
│   └── IStudentRepository.cs  ← required interface
├── Services/
│   ├── MemoryStudentRepository.cs   ← 5 real students
│   ├── StubStudentRepository.cs     ← Task 3: test data only
│   ├── AverageCalculator.cs         ← DRY: single place for avg formula
│   ├── StudentValidator.cs
│   ├── StudentService.cs            ← depends on IStudentRepository
│   └── ReportService.cs             ← Task 2: LINQ + no-LINQ methods
├── UI/
│   └── ConsoleMenu.cs              ← ONLY place Console.Write is used
└── LINQ_Drills.cs                  ← excluded from compilation
```

---

## How to Run

### Normal mode (5 real students)
```bash
dotnet run
```

### Stub mode (1 test student in group TEST)
```bash
dotnet run -- --stub
```

---

## Task Descriptions

### Task 1 — Layers
- `Models/` — pure data classes (no logic)
- `Services/` — all business logic; `StudentService` depends on `IStudentRepository` (not the concrete class)
- `UI/` — `ConsoleMenu` is the only class that calls `Console.Write/ReadLine`
- Constructor injection used everywhere — no static state

### Task 2 — LINQ vs. No-LINQ (menu items 7, 8, 9)
In `ReportService`:
- `GetTopByAverage` / `GetTopByAverageWithoutLinq`
- `GetStudentsInGroupSortedByName` / `GetStudentsInGroupSortedByNameWithoutLinq`
- `GetStatistics` / `GetStatisticsWithoutLinq`

Menu items 7/8/9 print both versions side-by-side so equivalence is visible.

### Task 3 — Runtime arguments
- `dotnet run` → `MemoryStudentRepository` (5 students)
- `dotnet run -- --stub` → `StubStudentRepository` (1 student: Id=999, "Test Student", group "TEST")
- Switching happens in `Program.cs` only; services never check which repo they got.

### Drills
`LINQ_Drills.cs` contains 5 drills, each with a LINQ version and a `_Plain` version without LINQ operators. The file is excluded from the main build (see `.csproj`) so it doesn't interfere.
