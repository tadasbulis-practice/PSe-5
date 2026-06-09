# OOP Lab 6, Mudar Shawakh, 09.06.2026

## Additions
* Introduced Repository, Printer, Validator, and Menu interfaces to act as discrete service containers.

## Changes from Lab 5
* Student grades are now encapsulated as `private`.
* `IAverageStrategy` has been refactored to operate on `IReadOnlyList<Student>` rather than the internal data structure.

## Removals
* Dropped `IStudentFinder`, Fake, and Stub classes as they are outside the scope of the Lab 6 specification.

## Pattern to Location Mapping
* **Repository Pattern:** Implemented in `MemoryStudentRepository`.
* **Strategy Pattern:** Utilized for Printers and Average Strategies.
* **Abstractions:** Applied to Validator and Menu Service implementations.

## Design Justification
This architectural design strictly adheres to Lab 6 constraints. It relies on private containers, exposes collections securely via `IReadOnlyList`, mandates constructor dependency injection, and ensures `Program.cs` remains clear of any business logic.