using System.ComponentModel;
using Lab10.Implementations.FileServices;
using Lab10.Interfaces;
using Lab10.Models;

namespace Lab10.Forms;

/// <summary>
/// Main window. Pure UI:
///   - DataGridView bound to a BindingList of students
///   - MenuStrip File menu: Save/Open as JSON or CSV
///   - Toolbar buttons: Add, Edit, Delete
/// All business logic stays in Repository / FileService classes.
/// </summary>
public class MainForm : Form
{
    private readonly IStudentRepository _repository;
    private readonly BindingList<Student> _students = new();

    // Strategy pattern: two file format implementations behind one interface.
    private readonly IStudentFileService _jsonFile = new JsonStudentFileService();
    private readonly IStudentFileService _csvFile  = new CsvStudentFileService();

    private readonly DataGridView _grid;

    public MainForm(IStudentRepository repository)
    {
        _repository = repository;

        Text  = "Student Management — Lab 10";
        Width = 1000; Height = 600;
        StartPosition = FormStartPosition.CenterScreen;

        // Build all controls first, then add them in REVERSE order
        // so docked controls (Fill, Top) layer correctly:
        //   Fill   → added first (occupies center after others claim their edges)
        //   Top×n  → added last; the last-added is the highest

        _grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            MultiSelect = false,
            DataSource = _students,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        };
        _grid.CellDoubleClick += (_, _) => EditSelected();

        var toolbar = BuildToolbar();
        var menu    = BuildMenu();
        MainMenuStrip = menu;

        Controls.Add(_grid);     // Fill in middle
        Controls.Add(toolbar);   // Top — sits below the menu
        Controls.Add(menu);      // Top — sits at the very top

        // 4) Initial load from repository
        ReloadFromRepository();
    }

    private MenuStrip BuildMenu()
    {
        var menu = new MenuStrip();

        var fileMenu = new ToolStripMenuItem("&File");

        var saveJson = new ToolStripMenuItem("Save as &JSON...", null, (_, _) => SaveTo(_jsonFile));
        var saveCsv  = new ToolStripMenuItem("Save as &CSV...",  null, (_, _) => SaveTo(_csvFile));
        var loadJson = new ToolStripMenuItem("Open JSON...",     null, (_, _) => LoadFrom(_jsonFile));
        var loadCsv  = new ToolStripMenuItem("Open CSV...",      null, (_, _) => LoadFrom(_csvFile));
        var exit     = new ToolStripMenuItem("E&xit",            null, (_, _) => Close());

        fileMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            saveJson, saveCsv,
            new ToolStripSeparator(),
            loadJson, loadCsv,
            new ToolStripSeparator(),
            exit,
        });

        menu.Items.Add(fileMenu);
        return menu;
    }

    private ToolStrip BuildToolbar()
    {
        var bar = new ToolStrip();

        var add = new ToolStripButton("Add",    null, (_, _) => AddNew())
        { DisplayStyle = ToolStripItemDisplayStyle.Text };
        var edit = new ToolStripButton("Edit",  null, (_, _) => EditSelected())
        { DisplayStyle = ToolStripItemDisplayStyle.Text };
        var del  = new ToolStripButton("Delete", null, (_, _) => DeleteSelected())
        { DisplayStyle = ToolStripItemDisplayStyle.Text };

        bar.Items.AddRange(new ToolStripItem[] { add, edit, del });
        return bar;
    }

    // ── Data sync helpers ───────────────────────────────────────

    private void ReloadFromRepository()
    {
        _students.Clear();
        foreach (var s in _repository.GetAll()) _students.Add(s);
    }

    // ── Add / Edit / Delete actions ────────────────────────────

    private void AddNew()
    {
        using var dlg = new StudentEditForm();
        if (dlg.ShowDialog(this) != DialogResult.OK || dlg.Result is null) return;

        if (_repository.GetById(dlg.Result.Id) != null)
        {
            MessageBox.Show($"A student with Id {dlg.Result.Id} already exists.",
                "Duplicate Id", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _repository.Add(dlg.Result);
        _students.Add(dlg.Result);
    }

    private void EditSelected()
    {
        var current = SelectedStudent();
        if (current is null) return;

        using var dlg = new StudentEditForm(current);
        if (dlg.ShowDialog(this) != DialogResult.OK || dlg.Result is null) return;

        _repository.Remove(current.Id);
        _repository.Add(dlg.Result);
        var idx = _students.IndexOf(current);
        if (idx >= 0) _students[idx] = dlg.Result;
    }

    private void DeleteSelected()
    {
        var s = SelectedStudent();
        if (s is null) return;

        var confirm = MessageBox.Show(
            $"Delete student {s.FullName} (Id {s.Id})?",
            "Confirm delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes) return;

        _repository.Remove(s.Id);
        _students.Remove(s);
    }

    private Student? SelectedStudent()
    {
        if (_grid.CurrentRow?.DataBoundItem is Student s) return s;
        return null;
    }

    // ── File save / load (Strategy: JSON or CSV) ────────────────

    private void SaveTo(IStudentFileService service)
    {
        using var dlg = new SaveFileDialog
        {
            Title    = $"Save students as {service.DisplayLabel}",
            Filter   = $"{service.DisplayLabel} (*{service.FileExtension})|*{service.FileExtension}|All files (*.*)|*.*",
            FileName = $"students{service.FileExtension}",
        };
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            service.Save(_students.ToList(), dlg.FileName);
            MessageBox.Show($"Saved {_students.Count} students to:\n{dlg.FileName}",
                "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message, "Could not save",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadFrom(IStudentFileService service)
    {
        using var dlg = new OpenFileDialog
        {
            Title  = $"Open {service.DisplayLabel}",
            Filter = $"{service.DisplayLabel} (*{service.FileExtension})|*{service.FileExtension}|All files (*.*)|*.*",
        };
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            var loaded = service.Load(dlg.FileName);
            ReplaceAll(loaded);
            MessageBox.Show($"Loaded {loaded.Count} students from:\n{dlg.FileName}",
                "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message, "Could not open",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ReplaceAll(IEnumerable<Student> loaded)
    {
        // Re-seed the in-memory repository AND the bound list.
        var existingIds = _repository.GetAll().Select(s => s.Id).ToList();
        foreach (var id in existingIds) _repository.Remove(id);

        _students.Clear();
        foreach (var s in loaded)
        {
            _repository.Add(s);
            _students.Add(s);
        }
    }
}
