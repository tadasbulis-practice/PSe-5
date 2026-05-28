CW‑1 — Object‑Oriented Programming in C#
Student: John Bastille
Date: 28 May 2026

Overview
This project is the refactored version of the CW‑1 assignment.
The original program contained all logic inside a single Program.cs file (~250 lines).
This version restructures the application into a clean, layered OOP architecture using:

Models

Interfaces

Services

UI

Constructor Injection

LINQ + No‑LINQ implementations

Runtime argument–based repository switching

The solution fully implements Task 1, Task 2, Task 3, and the LINQ Drills.

Tasks Completed
✅ Task 1 — Refactor into Layers
The project now follows the required structure:

Code
CW1After/
 ├── Program.cs
 ├── Models/
 │    ├── Student.cs
 │    └── Group.cs
 ├── Interfaces/
 │    └── IStudentRepository.cs
 │    └── IStudentService.cs
 ├── Services/
 │    ├── InMemoryStudentRepository.cs
 │    ├── StubStudentRepository.cs
 │    ├── StudentService.cs
 │    ├── StudentValidator.cs
 │    ├── AverageCalculator.cs
 │    └── ReportService.cs
 └── UI/
      └── ConsoleMenu.cs
OOP Principles Applied
SRP: Each class has one responsibility

DI: All services receive dependencies via constructors

Abstraction: StudentService and ReportService depend on IStudentRepository

UI Separation: All console I/O is isolated in ConsoleMenu

DRY: Average calculation and validation logic centralized

✅ Task 2 — LINQ → No‑LINQ Implementations
Inside ReportService, the following pairs are implemented:

1. Top N by average
Top3ByAverage_Linq()

Top3ByAverage_NoLinq()

2. Students in group sorted by name
StudentsInGroup_Linq(string)

StudentsInGroup_NoLinq(string)

3. Statistics
Statistics_Linq()

Statistics_NoLinq()

Restrictions Followed
No-LINQ versions use only:
for, foreach, if, List<T>.Add, List<T>.Sort, Count property

No forbidden LINQ operators used (Where, Select, OrderBy, etc.)

✅ Task 3 — Runtime Arguments + Stub Repository
The program supports switching data sources at runtime.

Usage
Default (in‑memory repository):

Code
dotnet run
Stub repository:

Code
dotnet run -- --stub
Startup Messages
[INFO] Using MemoryStudentRepository (default).

[INFO] Using StubStudentRepository (--stub).

Stub Repository Contents
Group: TEST

Student:

ID: 999

Name: Test Student

Email: test@test.lt

Grades: {10, 10, 10}

✅ Drills — LINQ Exercises
All 5 drills in LINQ_Drills.cs include:

LINQ version (given)

Plain version (implemented)

Drills cover:

Filtering

Sorting + Take

Sum + Average

Boolean aggregates

Full pipeline (Where + OrderBy + Select)

How to Run the Program
Normal mode (real data)
Code
dotnet run
Stub mode (test data)
Code
dotnet run -- --stub
Menu Options
List all students

Add new student

Add grade

Show average

Find by ID

Validate student

Top 3 by average (LINQ)

Students in group (LINQ)

Statistics (LINQ)

Exit

Notes
All logic is cleanly separated into layers

No console I/O exists in services

No global static lists

No repository creation inside UI

All DI is constructor‑based

Project builds and runs in both modes