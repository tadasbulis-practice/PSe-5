# CW1 - Malek Krimi

Date: 2026-05-21

## Completed tasks

- Task 1: Refactored the starter `Program.cs` into Models, Interfaces, Services, and UI layers.
- Task 2: Added LINQ and no-LINQ versions for top students, group sorting, and statistics in `Services/ReportService.cs`.
- Task 3: Added `StubStudentRepository` and runtime argument switching with `--stub`.
- Drills: Completed all five plain/no-LINQ drill methods in `LINQ_Drills.cs`.

## Project structure

```text
CW1After/
├── Program.cs
├── Models/
│   ├── Student.cs
│   └── Group.cs
├── Interfaces/
│   └── IStudentRepository.cs
├── Services/
│   ├── MemoryStudentRepository.cs
│   ├── StubStudentRepository.cs
│   ├── StudentValidator.cs
│   ├── AverageCalculator.cs
│   ├── StudentService.cs
│   └── ReportService.cs
└── UI/
    └── ConsoleMenu.cs
```

## How to run

Default memory repository:

```bash
dotnet run
```

Stub repository:

```bash
dotnet run -- --stub
```

## Expected stub result

When running with `--stub`, the program should show:

```text
[INFO] Using StubStudentRepository (--stub).
```

Menu item 1 should list only:

```text
[999] Test Student (TEST) avg=10.00
```

## Where to look for the results

- Menu items 1-6: regular student operations.
- Menu item 7: top 3 students by average, shown using LINQ and without LINQ.
- Menu item 8: students in selected group, sorted by name, shown using LINQ and without LINQ.
- Menu item 9: statistics, shown using LINQ and without LINQ.
- `LINQ_Drills.cs`: five completed plain/no-LINQ drill methods.
