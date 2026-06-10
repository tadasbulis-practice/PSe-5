# Lab-8 — SOLID Principles in OOP C#

Extends Lab-7 by applying all five SOLID principles to the existing architecture.

---

## Architecture

```
Program.cs  (Composition Root — DIP in action)
    │
    ├── IMenuService       → ConsoleMenuService
    ├── ISolidDemoService  → SolidDemoService        ← NEW (ISP)
    └── StudentService
            ├── IStudentRepository  → MemoryStudentRepository | ApiStudentRepository
            ├── IStudentPrinter     → ConsoleStudentPrinter | FileStudentPrinter  ← NEW (OCP, SRP)
            ├── IAverageStrategy    → Simple | Weighted | Median strategies        ← NEW (OCP)
            └── IStudentValidator   → StudentValidatorAdapter | StrictStudentValidator ← NEW (LSP)
```

---

## SOLID Principles — What was applied

### S — Single Responsibility
Every class has exactly one reason to change:
- `ConsoleStudentPrinter` → only prints to console
- `FileStudentPrinter` → only prints to file
- `StudentService` → only orchestrates business logic
- `MemoryStudentRepository` → only stores/retrieves data

### O — Open/Closed
System is open for extension, closed for modification:
- Added `WeightedAverageStrategy` and `MedianAverageStrategy` without touching `StudentService`
- Added `FileStudentPrinter` without touching `ConsoleStudentPrinter`

### L — Liskov Substitution
Any implementation can replace its interface without breaking the system:
- `StrictStudentValidator` can replace `StudentValidatorAdapter` everywhere
- `ApiStudentRepository` can replace `MemoryStudentRepository` by changing one line in `Program.cs`

### I — Interface Segregation
Small, focused interfaces — no class implements methods it doesn't need:
- `IMenuService` → 1 method
- `IAverageStrategy` → 1 method
- `ISolidDemoService` → 1 method
- `IStudentPrinter` → 3 methods (all related to printing)

### D — Dependency Inversion
High-level modules depend on abstractions:
- `StudentService` never references `MemoryStudentRepository`, `ConsoleStudentPrinter`, etc.
- `Program.cs` is the only place concrete classes appear

---

## New files added in Lab-8

| File | Principle demonstrated |
|------|----------------------|
| `Implementations/Strategy/WeightedAverageStrategy.cs` | OCP |
| `Implementations/Strategy/MedianAverageStrategy.cs` | OCP |
| `Implementations/Printer/FileStudentPrinter.cs` | SRP + OCP |
| `Implementations/Validator/StrictStudentValidator.cs` | LSP |
| `Interfaces/ISolidDemoService.cs` | ISP |
| `Services/SolidDemoService.cs` | All 5 demonstrated |

---

## How to run

```bash
cd Lab8
dotnet run
```

Select option **7** from the menu to run the full SOLID demo.

To use the real Docker API instead of demo data:
1. Start Docker: `docker compose up` (from the lab-8-docker folder)
2. Set `const bool useApi = true;` in `Program.cs`
3. Run again
