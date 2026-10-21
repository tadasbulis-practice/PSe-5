# Lab-10 Cross-Platform Setup Guide (Avalonia)

This guide is for **macOS** and **Linux** students (and Windows students who
prefer the cross-platform path). The WinForms project does not run on Mac
or Linux — use this Avalonia version instead.

The Avalonia project is the same Lab-10 architecture: only the View layer
changes. Everything you learned in Weeks 3–9 still applies.

---

## 1. Install .NET 8 SDK (one-time)

| OS                 | How                                                                  |
|--------------------|----------------------------------------------------------------------|
| **macOS (Apple Silicon)** | `brew install --cask dotnet-sdk`  *or*  download the **arm64** installer from <https://dotnet.microsoft.com/download/dotnet/8.0> |
| **macOS (Intel)**  | `brew install --cask dotnet-sdk`  *or*  the **x64** installer        |
| **Ubuntu / Debian**| `sudo apt install dotnet-sdk-8.0`                                    |
| **Fedora**         | `sudo dnf install dotnet-sdk-8.0`                                    |
| **Windows**        | Already have it (winget: `winget install Microsoft.DotNet.SDK.8`)    |

Verify:

```bash
dotnet --version
# → 8.0.xxx
```

---

## 2. Open the project

Unzip `Lab-10-CrossPlatform-Avalonia.zip` somewhere convenient.
Open the folder in your IDE:

- **JetBrains Rider** — File → Open → pick `Lab10.csproj`
- **VS Code** — install the *C# Dev Kit* extension, then open the folder
- **Visual Studio (Windows)** — File → Open → Project/Solution → `Lab10.csproj`

---

## 3. Run

From any terminal in the project folder:

```bash
dotnet run --project Lab10.csproj
```

The first run downloads the Avalonia and MsBox.Avalonia packages
(~30 MB total) and starts the window.

You should see the same Lab-10 app: a grid of students, an Add/Edit/Delete
toolbar, and a File menu with Save/Open as JSON / CSV.

---

## 4. Comparison table — WinForms vs Avalonia

| If you saw this in the Lab-10 lecture (WinForms) | Find it here (Avalonia)                |
|--------------------------------------------------|----------------------------------------|
| `Form`                                           | `Window`                               |
| `BindingList<Student>`                           | `ObservableCollection<Student>`        |
| `DataGridView`                                   | `DataGrid` (Avalonia.Controls.DataGrid)|
| `MenuStrip`                                      | `Menu`                                 |
| `ToolStrip`                                      | `StackPanel` of `Button`s              |
| `MessageBox.Show(...)`                           | `MsBox.Avalonia` (`await box.ShowWindowDialogAsync(this)`) |
| `SaveFileDialog`                                 | `StorageProvider.SaveFilePickerAsync`  |
| `OpenFileDialog`                                 | `StorageProvider.OpenFilePickerAsync`  |
| `dlg.ShowDialog()` returning `DialogResult.OK`   | `await dlg.ShowDialog(owner)` then check `dlg.Result` |

`Models/`, `Interfaces/`, `Implementations/`, `Exceptions/` are **identical**
to the WinForms project. That is the entire point — pure OOP layers don't
care which GUI library sits on top.

---

## 5. Common issues

**"The framework 'Microsoft.WindowsDesktop.App' was not found"**
You opened the WinForms project, not the Avalonia one. Use `Lab10.csproj`
from `Lab-10-CrossPlatform-Avalonia.zip`.

**Mac asks to install Rosetta**
You're on Apple Silicon but the .NET SDK is x64. Install the **arm64** SDK.

**App opens but the grid is empty**
That's normal on first launch — `MemoryStudentRepository` seeds three
demo students, but if you replaced it with `useApi=true` and the Docker
API is not running, the fallback in `Program.cs` will show an empty
in-memory list. Press **Add** to insert a student.

**`dotnet run` warns "An assembly specified in the application
dependencies manifest (... .deps.json) was not found"**
Run `dotnet restore` once before `dotnet run`.

---

## 6. What if I want both WinForms and Avalonia?

You can keep both projects side by side. The shared code
(`Models/`, `Interfaces/`, `Implementations/`, `Exceptions/`) is
identical in both — in a real project you would extract it into a
shared class library (`Lab10.Core.csproj`) referenced by both views.
That is exactly what the Open/Closed Principle predicts: views come
and go, the core stays.

---

*This is bonus material for Week-10 — the lecture and lab tasks are
the same as the WinForms version. You only need to choose ONE View
implementation to submit.*
