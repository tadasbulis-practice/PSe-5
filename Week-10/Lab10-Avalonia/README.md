# Lab-10 — Cross-Platform GUI (Avalonia version)

This is the cross-platform sibling of the WinForms Lab-10 project.
It runs on **Windows, macOS, and Linux** with the same .NET 8 SDK.

## How to run (any OS)

```bash
dotnet run --project Lab10.csproj
```

The first run will download the Avalonia and MsBox.Avalonia packages.

> macOS users on Apple Silicon — make sure your `dotnet --version` is 8.x.
> If it is missing, install it from <https://dotnet.microsoft.com/download>.

## What changes vs. the WinForms version

| Concept                | WinForms (Windows-only)              | Avalonia (cross-platform)                   |
|------------------------|--------------------------------------|---------------------------------------------|
| TargetFramework        | `net8.0-windows`                     | `net8.0`                                    |
| `<UseWindowsForms>`    | `true`                               | (removed)                                   |
| Window class           | `Form`                               | `Window`                                    |
| Bound list             | `BindingList<Student>`               | `ObservableCollection<Student>`             |
| Grid                   | `DataGridView`                       | `DataGrid` (separate Avalonia package)      |
| Menu / Toolbar         | `MenuStrip`, `ToolStrip`             | `Menu`, `StackPanel` of `Button`s           |
| Modal dialog           | `form.ShowDialog()` returns enum     | `await window.ShowDialog(owner)`            |
| MessageBox             | `MessageBox.Show(...)`               | `MsBox.Avalonia` (async)                    |
| File dialogs           | `SaveFileDialog`, `OpenFileDialog`   | `Window.StorageProvider.SaveFilePickerAsync` |

Everything else — **Models, Repositories, FileServices, Exceptions** — is
**byte-identical** to the WinForms Lab10. That's the lesson: the OOP
boundaries we built in Weeks 3–5 paid off again. Swap a View, the rest
keeps working.

## Project layout

```
Lab10-Avalonia/
├─ Lab10.csproj                        ← net8.0, no <UseWindowsForms>
├─ App.cs                              ← Avalonia application bootstrap
├─ Program.cs                          ← composition root + AppBuilder
├─ Models/                             ← UNCHANGED
│  ├─ Student.cs
│  └─ GraduateStudent.cs
├─ Interfaces/                         ← UNCHANGED
│  ├─ IStudentRepository.cs
│  └─ IStudentFileService.cs
├─ Implementations/                    ← UNCHANGED
│  ├─ Repository/
│  │  ├─ MemoryStudentRepository.cs
│  │  └─ ApiStudentRepository.cs
│  └─ FileServices/
│     ├─ JsonStudentFileService.cs
│     └─ CsvStudentFileService.cs
├─ Exceptions/                         ← UNCHANGED
│  └─ RepositoryException.cs
└─ Views/                              ← swapped from Forms/
   ├─ MainWindow.cs                    ← was MainForm.cs
   └─ StudentEditWindow.cs             ← was StudentEditForm.cs
```

## Architecture in one picture

```
                ┌───────────────────────────────────┐
                │            MainWindow             │ (View — Avalonia UI)
                │     DataGrid + Menu + Buttons     │
                └──────────────┬────────────────────┘
                               │ depends on
                  ┌────────────┼─────────────────┐
                  │            │                 │
        IStudentRepository  IStudentFileService  StudentEditWindow
                  │            │
   ┌──────────────┴──┐         ├──────────────┐
   │                 │         │              │
 Memory…         Api…       JsonStudent…   CsvStudent…
```

`MainWindow` knows nothing about `HttpClient`, `File`, or `JsonSerializer`.
Same Open/Closed Principle as the WinForms version.

## Notes for students

- **Why Avalonia?** It's a free, community-driven .NET GUI toolkit
  that targets all desktop platforms (and even mobile / browser).
  JetBrains Rider, AvaloniaUI itself, and many open-source apps use it.
- **No XAML in this lab** — we built the UI in plain C#, line-for-line
  comparable to the WinForms version. Real Avalonia projects normally
  use AXAML files for layout; that's a Year-2 topic.
- **WinForms students** can compare `Forms/MainForm.cs` (Lab10) and
  `Views/MainWindow.cs` (this project) side by side — only the View
  layer differs.
