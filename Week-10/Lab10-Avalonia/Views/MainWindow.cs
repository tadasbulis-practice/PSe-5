using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Platform.Storage;
using Lab10.Implementations.FileServices;
using Lab10.Interfaces;
using Lab10.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Lab10.Views;

/// <summary>
/// MainWindow — Avalonia equivalent of WinForms MainForm.
///
/// Compare line-by-line with Forms/MainForm.cs in the WinForms project:
///   Form              →  Window
///   BindingList<T>    →  ObservableCollection<T>
///   DataGridView      →  DataGrid                 (separate Avalonia package)
///   MenuStrip         →  Menu                     (top-level container)
///   ToolStrip         →  StackPanel of Buttons
///   MessageBox.Show() →  MsBox.Avalonia (async)
///   SaveFileDialog    →  StorageProvider.SaveFilePickerAsync
///
/// All Models / Repositories / FileServices / Exceptions are unchanged.
/// </summary>
public class MainWindow : Window
{
    private readonly IStudentRepository _repository;
    private readonly ObservableCollection<Student> _students = new();

    // Strategy pattern: same two implementations behind the same interface.
    private readonly IStudentFileService _jsonFile = new JsonStudentFileService();
    private readonly IStudentFileService _csvFile  = new CsvStudentFileService();

    private readonly DataGrid _grid;

    public MainWindow(IStudentRepository repository)
    {
        _repository = repository;

        Title  = "Student Management — Lab 10 (Avalonia)";
        Width  = 1000;
        Height = 600;

        // ── DataGrid bound to the ObservableCollection ──────────────
        _grid = new DataGrid
        {
            AutoGenerateColumns = true,
            IsReadOnly          = true,
            SelectionMode       = DataGridSelectionMode.Single,
            CanUserResizeColumns = true,
            ItemsSource         = _students,
        };
        _grid.DoubleTapped += async (_, _) => await EditSelectedAsync();

        // ── Menu (File → Save/Open JSON/CSV, Exit) ──────────────────
        var fileMenu = new MenuItem { Header = "_File" };
        fileMenu.Items.Add(MakeMenuItem("Save as _JSON...", () => SaveToAsync(_jsonFile)));
        fileMenu.Items.Add(MakeMenuItem("Save as _CSV...",  () => SaveToAsync(_csvFile)));
        fileMenu.Items.Add(new Separator());
        fileMenu.Items.Add(MakeMenuItem("Open JSON...", () => LoadFromAsync(_jsonFile)));
        fileMenu.Items.Add(MakeMenuItem("Open CSV...",  () => LoadFromAsync(_csvFile)));
        fileMenu.Items.Add(new Separator());
        fileMenu.Items.Add(MakeMenuItem("E_xit", Close));

        var menu = new Menu();
        menu.Items.Add(fileMenu);

        // ── Toolbar (Add / Edit / Delete) ───────────────────────────
        var toolbar = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing     = 6,
            Margin      = new Thickness(6),
        };
        toolbar.Children.Add(MakeButton("Add",    async () => await AddNewAsync()));
        toolbar.Children.Add(MakeButton("Edit",   async () => await EditSelectedAsync()));
        toolbar.Children.Add(MakeButton("Delete", async () => await DeleteSelectedAsync()));

        // ── Layout: DockPanel — menu on top, toolbar below, grid fills ──
        var root = new DockPanel { LastChildFill = true };
        DockPanel.SetDock(menu,    Dock.Top);
        DockPanel.SetDock(toolbar, Dock.Top);
        root.Children.Add(menu);
        root.Children.Add(toolbar);
        root.Children.Add(_grid);
        Content = root;

        ReloadFromRepository();
    }

    // ── Helpers to keep the constructor short ──────────────────────
    private static MenuItem MakeMenuItem(string header, Func<Task> click)
    {
        var mi = new MenuItem { Header = header };
        mi.Click += async (_, _) => await click();
        return mi;
    }
    private static MenuItem MakeMenuItem(string header, Action click)
    {
        var mi = new MenuItem { Header = header };
        mi.Click += (_, _) => click();
        return mi;
    }
    private static Button MakeButton(string text, Func<Task> click)
    {
        var b = new Button { Content = text, Padding = new Thickness(12, 4) };
        b.Click += async (_, _) => await click();
        return b;
    }

    // ── Data sync ───────────────────────────────────────────────────
    private void ReloadFromRepository()
    {
        _students.Clear();
        foreach (var s in _repository.GetAll()) _students.Add(s);
    }

    private Student? SelectedStudent() => _grid.SelectedItem as Student;

    // ── Add / Edit / Delete ─────────────────────────────────────────
    private async Task AddNewAsync()
    {
        var dlg = new StudentEditWindow();
        await dlg.ShowDialog(this);
        if (dlg.Result is null) return;

        if (_repository.GetById(dlg.Result.Id) != null)
        {
            await ShowAsync($"A student with Id {dlg.Result.Id} already exists.",
                            "Duplicate Id", Icon.Warning);
            return;
        }

        _repository.Add(dlg.Result);
        _students.Add(dlg.Result);
    }

    private async Task EditSelectedAsync()
    {
        var current = SelectedStudent();
        if (current is null) return;

        var dlg = new StudentEditWindow(current);
        await dlg.ShowDialog(this);
        if (dlg.Result is null) return;

        _repository.Remove(current.Id);
        _repository.Add(dlg.Result);
        var idx = _students.IndexOf(current);
        if (idx >= 0) _students[idx] = dlg.Result;
    }

    private async Task DeleteSelectedAsync()
    {
        var s = SelectedStudent();
        if (s is null) return;

        var box = MessageBoxManager.GetMessageBoxStandard(
            "Confirm delete",
            $"Delete student {s.FullName} (Id {s.Id})?",
            ButtonEnum.YesNo, Icon.Question);
        var answer = await box.ShowWindowDialogAsync(this);
        if (answer != ButtonResult.Yes) return;

        _repository.Remove(s.Id);
        _students.Remove(s);
    }

    // ── File save / load (Strategy: JSON or CSV) ────────────────────
    private async Task SaveToAsync(IStudentFileService service)
    {
        var picked = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title              = $"Save students as {service.DisplayLabel}",
            SuggestedFileName  = $"students{service.FileExtension}",
            DefaultExtension   = service.FileExtension.TrimStart('.'),
            FileTypeChoices    = new[]
            {
                new FilePickerFileType(service.DisplayLabel)
                {
                    Patterns = new[] { "*" + service.FileExtension }
                }
            },
        });
        if (picked is null) return;

        try
        {
            service.Save(_students.ToList(), picked.Path.LocalPath);
            await ShowAsync($"Saved {_students.Count} students.",
                            "Saved", Icon.Info);
        }
        catch (IOException ex)
        {
            await ShowAsync(ex.Message, "Could not save", Icon.Error);
        }
    }

    private async Task LoadFromAsync(IStudentFileService service)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title           = $"Open {service.DisplayLabel}",
            AllowMultiple   = false,
            FileTypeFilter  = new[]
            {
                new FilePickerFileType(service.DisplayLabel)
                {
                    Patterns = new[] { "*" + service.FileExtension }
                }
            },
        });
        if (files.Count == 0) return;

        try
        {
            var loaded = service.Load(files[0].Path.LocalPath);
            ReplaceAll(loaded);
            await ShowAsync($"Loaded {loaded.Count} students.",
                            "Loaded", Icon.Info);
        }
        catch (IOException ex)
        {
            await ShowAsync(ex.Message, "Could not open", Icon.Error);
        }
    }

    private void ReplaceAll(IEnumerable<Student> loaded)
    {
        foreach (var id in _repository.GetAll().Select(s => s.Id).ToList())
            _repository.Remove(id);

        _students.Clear();
        foreach (var s in loaded)
        {
            _repository.Add(s);
            _students.Add(s);
        }
    }

    // ── MessageBox helper (Avalonia equivalent of WinForms MessageBox.Show) ──
    private Task ShowAsync(string text, string title, Icon icon)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, text, ButtonEnum.Ok, icon);
        return box.ShowWindowDialogAsync(this);
    }
}
