# Lab-10 — File I/O & WinForms

This solution is the reference implementation for Week-10 of `PS OOP C#`.
It extends the architecture from Lab-7..9 with:

- A WinForms graphical interface (replaces the Lab-9 console menu)
- Two file format implementations behind one interface
- Full exception handling all the way to friendly `MessageBox` dialogs

## How to run

```
dotnet run --project Lab10.csproj
```

By default the app starts with the in-memory repository. To switch to the
Docker REST API used in Lab-7, edit `Program.cs`:

```csharp
const bool useApi = true;     // ← set to true once Docker is up
```

## Project layout

```
Lab10/
├─ Lab10.csproj                 ← WinForms target (net8.0-windows)
├─ Program.cs                   ← composition root + API/Memory fallback
├─ Models/
│   ├─ Student.cs               ← Lab-9 validation, [JsonConstructor]
│   └─ GraduateStudent.cs       ← Lab-8 inheritance + ThesisTitle
├─ Interfaces/
│   ├─ IStudentRepository.cs
│   └─ IStudentFileService.cs   ← NEW — JSON & CSV implement this
├─ Implementations/
│   ├─ Repository/
│   │   ├─ MemoryStudentRepository.cs
│   │   └─ ApiStudentRepository.cs    ← wraps HTTP errors as RepositoryException
│   └─ FileServices/
│       ├─ JsonStudentFileService.cs  ← System.Text.Json (polymorphic)
│       └─ CsvStudentFileService.cs   ← Manual CSV with type discriminator
├─ Exceptions/
│   └─ RepositoryException.cs
└─ Forms/
    ├─ MainForm.cs              ← DataGridView + MenuStrip + toolbar
    └─ StudentEditForm.cs       ← Modal Add/Edit (validation via constructor)
```

## What the user can do

1. **See all students** — DataGridView is bound to a `BindingList<Student>` so
   any change to the list is reflected immediately.
2. **Add** a student — modal form. Validation errors raise an `ArgumentException`
   that is caught and shown as a `MessageBox.Warning`; the form stays open.
3. **Edit** the selected student (or double-click a row). Same modal form,
   pre-populated. Id is locked.
4. **Delete** the selected student — Yes/No confirmation.
5. **File → Save as JSON / CSV** — `SaveFileDialog`, format-specific extension.
6. **File → Open JSON / CSV** — `OpenFileDialog`, replaces the in-memory list.

All `IOException`s bubble up to `MainForm` and are surfaced via `MessageBox.Error`.

## Polymorphism note (JSON)

`JsonStudentFileService` registers `Student` and `GraduateStudent` as
polymorphic derived types via `DefaultJsonTypeInfoResolver`. The serialized
JSON includes a `"$type"` discriminator (`"Student"` or `"GraduateStudent"`),
so a list with mixed types round-trips correctly.

The CSV format uses a `Type` column (`STUD` / `GRAD`) for the same reason.

## Architecture in one picture

```
                ┌───────────────────────────────────┐
                │             MainForm              │ (View — UI only)
                │  DataGridView + MenuStrip         │
                └──────────────┬────────────────────┘
                               │ depends on
                  ┌────────────┼─────────────────┐
                  │            │                 │
        IStudentRepository  IStudentFileService  StudentEditForm
                  │            │
   ┌──────────────┴──┐         ├──────────────┐
   │                 │         │              │
 Memory…         Api…       JsonStudent…   CsvStudent…
```

`MainForm` knows nothing about `HttpClient`, `File`, or `JsonSerializer`.
It only knows the two interfaces.
