using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Lab10.Models;

namespace Lab10.Views;

/// <summary>
/// Modal Add/Edit dialog — Avalonia equivalent of WinForms StudentEditForm.
///
/// Same idea as WinForms:
///   - OK click constructs a new Student/GraduateStudent.
///   - The constructor enforces invariants → throws ArgumentException on bad input.
///   - We catch and show a MessageBox; the dialog stays open for fixes.
/// </summary>
public class StudentEditWindow : Window
{
    private readonly NumericUpDown _id   = new() { Minimum = 1, Maximum = 999_999, Value = 100 };
    private readonly TextBox       _firstName    = new();
    private readonly TextBox       _lastName     = new();
    private readonly TextBox       _email        = new();
    private readonly TextBox       _studyProgram = new();
    private readonly NumericUpDown _year = new() { Minimum = 2000, Maximum = 2100, Value = 2025 };
    private readonly CheckBox      _isGraduate   = new() { Content = "Graduate student" };
    private readonly TextBox       _thesisTitle  = new() { Watermark = "Thesis title" };

    /// <summary>Result is null if the user cancelled.</summary>
    public Student? Result { get; private set; }

    public StudentEditWindow() : this(null) { }

    public StudentEditWindow(Student? existing)
    {
        Title  = existing is null ? "Add student" : $"Edit student #{existing.Id}";
        Width  = 420;
        Height = 480;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        // Pre-fill if editing
        if (existing is not null)
        {
            _id.Value           = existing.Id;
            _id.IsEnabled       = false;
            _firstName.Text     = existing.FirstName;
            _lastName.Text      = existing.LastName;
            _email.Text         = existing.Email;
            _studyProgram.Text  = existing.StudyProgram;
            _year.Value         = existing.EnrollmentYear;
            if (existing is GraduateStudent g)
            {
                _isGraduate.IsChecked = true;
                _thesisTitle.Text     = g.ThesisTitle;
            }
        }

        // Hide / show thesis box based on the checkbox
        _thesisTitle.IsVisible = _isGraduate.IsChecked == true;
        _isGraduate.IsCheckedChanged += (_, _) =>
            _thesisTitle.IsVisible = _isGraduate.IsChecked == true;

        // Layout: simple StackPanel with labels on top of inputs
        var form = new StackPanel { Margin = new Thickness(16), Spacing = 6 };
        form.Children.Add(new TextBlock { Text = "Id" });
        form.Children.Add(_id);
        form.Children.Add(new TextBlock { Text = "First name" });
        form.Children.Add(_firstName);
        form.Children.Add(new TextBlock { Text = "Last name" });
        form.Children.Add(_lastName);
        form.Children.Add(new TextBlock { Text = "Email" });
        form.Children.Add(_email);
        form.Children.Add(new TextBlock { Text = "Study program" });
        form.Children.Add(_studyProgram);
        form.Children.Add(new TextBlock { Text = "Enrollment year" });
        form.Children.Add(_year);
        form.Children.Add(_isGraduate);
        form.Children.Add(_thesisTitle);

        // Buttons row
        var ok     = new Button { Content = "OK",     Padding = new Thickness(20, 4) };
        var cancel = new Button { Content = "Cancel", Padding = new Thickness(20, 4) };
        ok.Click     += async (_, _) => await OnOkClickAsync();
        cancel.Click += (_, _) => { Result = null; Close(); };

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing     = 8,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(16, 8),
        };
        buttons.Children.Add(ok);
        buttons.Children.Add(cancel);

        var root = new DockPanel { LastChildFill = true };
        DockPanel.SetDock(buttons, Dock.Bottom);
        root.Children.Add(buttons);
        root.Children.Add(form);
        Content = root;
    }

    private async Task OnOkClickAsync()
    {
        try
        {
            // NumericUpDown.Value is decimal? in Avalonia 11 — unwrap first.
            int id   = decimal.ToInt32(_id.Value   ?? 0);
            int year = decimal.ToInt32(_year.Value ?? 0);

            // Constructor does the validation — same pattern as WinForms version.
            Result = _isGraduate.IsChecked == true
                ? new GraduateStudent(
                    id,
                    _firstName.Text    ?? "",
                    _lastName.Text     ?? "",
                    _email.Text        ?? "",
                    _studyProgram.Text ?? "",
                    year,
                    _thesisTitle.Text  ?? "")
                : new Student(
                    id,
                    _firstName.Text    ?? "",
                    _lastName.Text     ?? "",
                    _email.Text        ?? "",
                    _studyProgram.Text ?? "",
                    year);

            Close();
        }
        catch (ArgumentException ex)
        {
            await MessageDialog.ShowAsync(this,
                "Validation error", ex.Message, DialogIcon.Warning);
            // Stay open so the user can fix the input.
        }
    }
}
