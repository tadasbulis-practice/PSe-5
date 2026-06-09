"# OOP Lab 7"
Full Name: Mudar Shawakh
Date: 09.06.2026
Task: SOLID + Repository over API

## LAB-7: SOLID Principles + ApiStudentRepository

**Patterns Implemented:** Repository Pattern, Strategy Pattern, Adapter Pattern
**Domain:** Faculty / Group / Student management with two interchangeable data sources

**Explanation:**
LAB-7 demonstrates all five SOLID principles by adding a second repository implementation, `ApiStudentRepository`, that loads faculty, group and student data from a REST API instead of memory. The entire architecture is unchanged when switching between the two repositories — only ONE line in `Program.cs` changes (`useApi = false` → `useApi = true`). The API is a separate ASP.NET Core project that runs inside Docker.

## Project structure
Lab7/
├── docker-compose.yml          (orchestrates the API container)
├── README.md                   (this file)
├── Lab7.App/                   (the OOP console application)
│   ├── Models/                 (Student, Group, Faculty)
│   ├── DTOs/                   (Api{Student,Group,Faculty}Dto)
│   ├── Interfaces/             (5 interfaces — Repository, Printer, Strategy, Validator, Menu)
│   ├── Legacy/                 (LegacyStudentValidation — a fake "old library")
│   ├── Services/               (StudentService — the orchestrator)
│   └── Implementations/
│       ├── Adapter/            (StudentValidatorAdapter wraps the legacy class)
│       ├── Menu/               (ConsoleMenuService)
│       ├── Printer/            (ConsoleStudentPrinter)
│       ├── Repository/         (MemoryStudentRepository + ApiStudentRepository)
│       └── Strategy/           (SimpleAverageStrategy)
└── Lab7.Api/                   (ASP.NET Core minimal API, dockerized)
├── Models/                 (FacultyResponse, GroupResponse, StudentResponse)
├── Program.cs              (4 endpoints — /, /api/faculty, /api/groups, /api/groups/{code})
└── Dockerfile              (multi-stage build, runs on port 6001)

## SOLID demonstration

1. **SRP — Single Responsibility:** every class has ONE job.
   `MemoryStudentRepository` stores, `StudentValidator` validates, `ConsoleStudentPrinter` prints, `SimpleAverageStrategy` calculates, `StudentService` orchestrates. Changing how printing works does not touch any other class.

2. **OCP — Open / Closed:** `ApiStudentRepository` was ADDED to the project — nothing existing was MODIFIED. `StudentService`, the menu, the validator and the strategy were not changed when the new repository was introduced.

3. **LSP — Liskov Substitution:** `MemoryStudentRepository` and `ApiStudentRepository` are fully substitutable. `StudentService` literally cannot tell them apart. Flipping `useApi` from false to true changes the data source without changing behaviour.

4. **ISP — Interface Segregation:** each interface is small and focused. `ConsoleStudentPrinter` implements only `IStudentPrinter`. It is not forced to know about repositories, strategies or validators.

5. **DIP — Dependency Inversion:** `StudentService` depends only on the four interfaces. The composition root in `Program.cs` is the only file aware of concrete classes. Swapping a printer, strategy or repository is a one-line change there.

## Adapter pattern (carried over from LAB-5)

`LegacyStudentValidation` represents an old library with an incompatible signature: `bool CheckStudent(string fullName, string email)`. `StudentValidatorAdapter` implements the modern `IStudentValidator` interface and translates the call. `StudentService` never knows the legacy class exists.

## How to run

### Without Docker (default — uses MemoryStudentRepository)
cd Lab7.App
dotnet run

### With Docker (uses ApiStudentRepository)
1. Open `Lab7.App/Program.cs` and change `useApi` to `true`.
2. Start the API container:
3. In a second terminal:
    Expected first lines when `useApi = true`:
    [Api] Connecting to backend...
    [Api] Loaded 5 students across 2 groups.

## Why this design

- The container lives in the repository; it is never exposed.
- DTOs only touch domain objects inside `ApiStudentRepository.MapDtoToDomain` — no DTO leaks out.
- `StudentService` has no field of type `ApiStudentRepository` or `MemoryStudentRepository` anywhere — only `IStudentRepository`.
- `Program.cs` is purely composition: pick implementations, wire them up, call `menu.Run()`.