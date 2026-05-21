# Control Work 1 (CW-1)
## OOP C# — Layers, Interfaces, LINQ, and Runtime Arguments

**Subject:** Object-Oriented Programming in C#
**Time:** 90 min (Task 1) + 30–40 min (Task 2 + Task 3) + 15–20 min (Drills)
**Layers:** Models / Services / UI + at least **one Interface**

---

## 🎯 Overall goal

You are given a working C# `Console` project **CW-1**. The program works, but **all the code** is jammed into a single `Program.cs` file (~250 lines), uses **LINQ** in spots where you must show that you understand "how it works under the hood", and has only one data source hard-wired in `Main()`.

The exam has four parts (all in the same submission):

| Part | Name | Goal | Points |
|------|------|------|--------|
| **Task 1** | Refactor into layers | Split `Program.cs` into `Models/Services/UI` + at least one Interface | 5 |
| **Task 2** | LINQ → loops | For menu items 7, 8, 9 write **two** versions — with and without LINQ | 2 |
| **Task 3** | Runtime args + Stub | Implement a `StubStudentRepository` and switch the source via a command-line argument | 2 |
| **Drills** | LINQ exercises (5 mini-tasks) | Additional `LINQ_Drills.cs` with 5 short snippets | 1 |

**Total: 10 points.**

---

## 📦 What you are given

```
CW-1/                          ← your starting point
├── CW1.sln
└── CW1/
    ├── CW1.csproj             (LINQ_Drills.cs is excluded from compilation)
    ├── Program.cs             (~250 lines, the "God class" with LINQ)
    └── LINQ_Drills.cs         (optional drills with TODO blocks)
```

---

## ✅ Task 1 — Structure and OOP (5 points)

### Requirements

Reorganize the project into **3 layers** plus an `Interfaces/` folder:

```
CW-1-after/
└── CW1After/
    ├── CW1After.csproj
    ├── Program.cs              ← composition root (parses args)
    ├── Models/                 ← pure data
    │   ├── Student.cs
    │   └── Group.cs
    ├── Interfaces/             ← at least ONE interface (required)
    │   └── IStudentRepository.cs
    ├── Services/               ← business logic
    │   ├── MemoryStudentRepository.cs
    │   ├── StubStudentRepository.cs    ← Task 3
    │   ├── StudentValidator.cs
    │   ├── AverageCalculator.cs
    │   ├── StudentService.cs
    │   └── ReportService.cs            ← Task 2
    └── UI/                     ← console interaction
        └── ConsoleMenu.cs
```

### Principles

- **SRP** — each class has **one** responsibility.
- **Constructor injection** — every service receives its dependencies through the constructor.
- **Abstraction through an interface** — `StudentService` depends on `IStudentRepository`.
- **DRY** — the average formula lives in one place.
- **UI separation** — `Console.WriteLine` / `Console.ReadLine` allowed **only** inside `UI/`.

### Menu functionality (items 1–6 are unchanged)

1. List all students
2. Add new student
3. Add grade
4. Show average
5. Find by id
6. Validate student
0. Exit

---

## ✅ Task 2 — LINQ → loops (2 points)

CW-1 menu items **7, 8, 9** are written **only with LINQ**. For each feature, provide **two implementations** inside the `ReportService` class:

| LINQ method | No-LINQ method | What it does |
|-------------|----------------|--------------|
| `GetTopByAverage(int n)` | `GetTopByAverageWithoutLinq(int n)` | Top N students by average |
| `GetStudentsInGroupSortedByName(string code)` | `GetStudentsInGroupSortedByNameWithoutLinq(string code)` | Students in a group, sorted by name |
| `GetStatistics()` | `GetStatisticsWithoutLinq()` | Count / Sum / Average / Max / Any / All |

### Restrictions for the no-LINQ versions

❌ **Not allowed**: `Where`, `Select`, `OrderBy`, `OrderByDescending`, `Take`, `Sum`, `Average`, `Count(lambda)`, `Any`, `All`, `Max`, `Min`, `SelectMany`, `FirstOrDefault(lambda)`, `GroupBy`.

✅ **Allowed**: `for`, `foreach`, `if`, `List<T>.Add/Remove`, `List<T>.Count` (the property), `List<T>.Sort` with a `Comparison<T>` delegate.

### Demonstration
In `ConsoleMenu` items 7/8/9, print **both** outputs back-to-back ("LINQ" and "no-LINQ") so equivalence is obvious.

---

## ✅ Task 3 — Runtime arguments + Stub repository (2 points)

### Motivation

In real projects you often need to launch the program against a **different data source** — e.g. for tests, demos, or training. Editing `MemoryStudentRepository → StubStudentRepository` by hand in the code is bad. The clean approach is to switch via a **command-line argument**, using the same `IStudentRepository` interface.

We started this idea in Week-4 (Fake / Stub) — now apply it for real in this project.

### Requirements

#### 3.1. Create `Services/StubStudentRepository.cs`

The class must:

- Implement **the same** `IStudentRepository` interface.
- Return **only one group** with code `"TEST"` and name `"Test group"`.
- Return **only one student**: `Id = 999`, `Name = "Test Student"`, `Email = "test@test.lt"`, `GroupCode = "TEST"`, grades `{ 10, 10, 10 }`.
- Any `Add(...)` call may throw `NotSupportedException` or simply do nothing — stubs typically **don't persist** anything.

#### 3.2. Read command-line arguments in `Program.cs`

```csharp
public static void Main(string[] args)
{
    // 1) parse args
    bool useStub = args.Contains("--stub");

    // 2) conditional injection
    IStudentRepository repository = useStub
        ? new StubStudentRepository()
        : new MemoryStudentRepository();

    // 3) rest of the composition root stays the same
    // ...
}
```

#### 3.3. Tell the user which repository is in use

`ConsoleMenu` or `Program.cs` must print on startup:
```
[INFO] Using StubStudentRepository (--stub).
```
or
```
[INFO] Using MemoryStudentRepository (default).
```

### Testing

| Command | Expected result |
|---------|----------------|
| `dotnet run` | The usual 5 students from the Memory repo |
| `dotnet run -- --stub` | Only 1 student "Test Student" in group "TEST" |
| `dotnet run -- --stub` + menu 1 | One line: `[999] Test Student (TEST) avg=10.00` |

> 💡 **Note**: with `dotnet run`, program arguments are separated from `dotnet` arguments by a double-dash: `dotnet run -- --stub`.

### Restrictions

- Use **only constructor injection** — no `if (useStub) ...` inside `StudentService`.
- No static global storage. A single `IStudentRepository` instance lives for the lifetime of the program.
- `args.Contains("--stub")` or `args.Any(a => a == "--stub")` are fine here (the Task 2 restrictions apply only to the report code).

---

## ✅ Drills — LINQ exercises (1 point, bonus)

`LINQ_Drills.cs` contains **5 mini-tasks**. Each has the LINQ version already written; you must add the `*_Plain` version without LINQ:

| # | Operators | What we test |
|---|-----------|--------------|
| 1 | `Where`                          | filtering |
| 2 | `OrderByDescending` + `Take(3)`  | sorting + take first N |
| 3 | `Sum` + `Average`                | aggregation |
| 4 | `Count(λ)` + `Any` + `All`       | boolean aggregates |
| 5 | `Where` + `OrderBy` + `Select`   | full pipeline |

The drill file is **excluded from compilation** (`.csproj`), so unfinished TODOs do not break the `Program.cs` build.

---

## 📊 Grading rubric (out of 10)

| Area | Criterion | Points |
|------|-----------|--------|
| **Task 1** | Structure and layers (Models/Services/UI folders) | 1.5 |
| **Task 1** | Interface + constructor injection | 1.5 |
| **Task 1** | OOP principles (SRP / DRY / no global `static List`) | 1 |
| **Task 1** | Code cleanliness + working code (menu items 1–6) | 1 |
| **Task 2** | LINQ versions work and produce correct output | 0.5 |
| **Task 2** | Non-LINQ versions work and produce the **same** output | 1 |
| **Task 2** | Restrictions respected (no LINQ in `*WithoutLinq` methods) | 0.5 |
| **Task 3** | `StubStudentRepository` implements `IStudentRepository` with test data | 1 |
| **Task 3** | `Program.cs` parses `args` and injects the chosen repository | 0.5 |
| **Task 3** | `dotnet run -- --stub` actually works and shows "Test Student" | 0.5 |
| **Drills** | At least 4 of 5 drills correctly written | 1 |

---

## 📤 Submission

The work is submitted in **two ways**: through Moodle (mandatory) and through the Git repository via a Pull Request.

### I. Moodle (mandatory)

1. Package the completed `solution`(s) into a ZIP archive, **excluding** the `bin/` and `obj/` folders.
2. Upload the archive to Moodle, under the **"Control Work"** assignment slot.
3. File name: **`CW1_Firstname_Lastname.zip`**.

> **No extra points** are awarded — this is a baseline submission requirement.

### II. Git repository — GitHub (via Pull Request)

**Repository:** [https://github.com/tadasbulis-practice/PS-5](https://github.com/tadasbulis-practice/PS-5)

1. From the `development` branch, create your own branch: **`feature/KD-Firstname-Lastname`**.
2. On the new branch, inside the repository folder **`KD/`**, create a sub-folder **`KD-Firstname-Lastname/`**.
3. Into that sub-folder:
   - **either** copy the provided `CW-1/` solution and do the tasks there,
   - **or** create a fresh `CW1` solution and do the tasks there.
4. In the solution (or project) root folder create a **`README.md`** containing:
   - your full name and the date;
   - which tasks (1 / 2 / 3 / Drills) you completed;
   - short explanations where helpful;
   - how to run the program (`dotnet run`, `dotnet run -- --stub`) and where to look for the results.
5. Push your changes to the remote (`git push`) and **only then** open a **Pull Request** targeting the `development` branch.
6. **Recommendation:** make a separate commit for each task with a descriptive title (e.g. `Task 1: extract Models layer`). Not required, but it helps the reviewer trace your work.

> The Pull Request submission timestamp is recorded as the work submission time.

---

## ⚠️ Rules

- Work **independently**. No copying from each other.
- You may consult your **own lecture notes**, **Lab-2** (architecture), **Lab-4** (Fake/Stub), **Lab-5** (interfaces), **Week-7 LINQ** material, and the official Microsoft C# documentation.
- **No** ChatGPT / Copilot / other AI tools during the exam.
- Questions about the **task wording** can be addressed to the lecturer.

---

## 💡 Hints

1. **Do Task 1 first** — move the models, then the interface and services.
2. **Do Task 3 right after Task 1** — `StubStudentRepository` is just a 30–40 line copy of `MemoryStudentRepository` but with 1 group and 1 student. The args parsing is 1 line.
3. **Do Task 2 last** — once the structure is in place, just add `ReportService` with the two sets of methods.
4. **Drills** are best done as a "cool-down" after Task 2 — each drill is very short.
5. **Verify both sides of Task 3** — run `dotnet run` and `dotnet run -- --stub`; both must work.
6. **No-LINQ Top-3 idea**: copy the list, find max, remove it, repeat 3 times.

**Good luck!** 🚀
