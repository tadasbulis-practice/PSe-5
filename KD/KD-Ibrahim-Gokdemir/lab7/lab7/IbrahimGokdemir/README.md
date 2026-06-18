# Lab 7 — Containers Deep: Generics, IRepository<T>, LINQ

Builds on Lab 6. The console app is renamed `Lab7.App`; the Docker API/Frontend
pair from the previous lab is reused and extended.

## What changed since Lab 6

| Lab 6 | Lab 7 |
|---|---|
| `IStudentRepository` had its own `Add/GetById/GetAll/Remove` | `IStudentRepository : IRepository<Student>` — those four methods now come from the generic interface |
| `MemoryStudentRepository` reimplemented a `List<Student>` + linear search | `MemoryRepository<T>` (generic, `Dictionary<int,T>`, O(1)) is written once; `MemoryStudentRepository` composes it |
| Only one repository (in-memory) | Two repositories behind the same interface: `MemoryStudentRepository` (fallback) and `ApiStudentRepository` (calls `SimpleStudentApi` over HTTP) |
| `GetAll()` returned `List<Student>` | Returns `IReadOnlyList<Student>` everywhere (repository, validator, printer, strategy) — callers can't `.Add()`/`.Clear()` by accident |
| `SimpleAverageStrategy` used two nested `foreach` loops | Rewritten with `SelectMany` + `Average` |
| No concept of Group/Faculty | `Student.GroupCode` + new `Group`/`Faculty` models, exposed via `IStudentRepository.GetGroupByCode/GetAllGroups/GetFaculty` |
| `StudentService` only printed students + one average | Adds a `GroupBy`-based group report and an `OrderByDescending`+`FirstOrDefault`-based "top student" lookup |

## SOLID / OOP principles applied

- **Single Responsibility** — `MemoryRepository<T>` only stores/retrieves; `MemoryStudentRepository` only adds the Student-specific Group/Faculty lookup on top.
- **Open/Closed** — `IRepository<T>` lets you add a repository for a brand-new entity (e.g. `Order`) without touching existing code, as long as that entity implements `IEntity`.
- **Liskov Substitution** — `MemoryStudentRepository` and `ApiStudentRepository` are interchangeable wherever `IStudentRepository` is expected; `StudentService` never knows which one it got.
- **Interface Segregation** — `IRepository<T>` only has the four generic CRUD members; Student-only concerns (Group/Faculty) live in `IStudentRepository`, not forced onto every entity.
- **Dependency Inversion** — `Program.cs` (composition root) decides the concrete repository; everything else depends on `IStudentRepository`.
- **DRY** — CRUD logic exists in exactly one place: `MemoryRepository<T>`.
- **Encapsulation** — internal `Dictionary`s in both repositories are private; only `IReadOnlyList<T>` leaves the class.

## Running in Memory mode (default, no Docker needed)

```bash
cd Lab7.App
dotnet run
```

`useApi` is `false` in `Program.cs`, so it seeds three sample students and runs entirely in memory — exactly like Lab 6, plus the new group report.

## Running in Api mode (against the Dockerized API)

1. Start the API (and frontend) containers from the repo root:

   ```bash
   docker compose up --build
   ```

2. In `Lab7.App/Program.cs`, set:

   ```csharp
   const bool useApi = true;
   ```

3. `dotnet run` again. `Lab7.App` now calls `http://localhost:6001/api/students`,
   `/api/groups`, `/api/faculty` instead of using seeded data. If the API isn't
   reachable, it logs a message and falls back to `MemoryStudentRepository`
   automatically — the rest of the pipeline doesn't change either way.

## SimpleStudentApi — new endpoints (Lab 7 addition)

The original `/api/info` endpoint is unchanged. New endpoints added on top:

- `GET /api/students` / `GET /api/students/{id}`
- `POST /api/students` / `DELETE /api/students/{id}`
- `GET /api/groups` / `GET /api/groups/{code}`
- `GET /api/faculty`

Backed by an in-memory list inside `SimpleStudentApi/Program.cs` — same three
sample students as `Lab7.App`'s memory fallback, so both modes show identical
data.

## SimpleStudentFrontend

Unchanged from Lab 6 — it only calls `/api/info` and isn't part of this
lab's repository-pattern requirements. Still works the same way:
`docker compose up --build`, then open `http://localhost:6011`.
