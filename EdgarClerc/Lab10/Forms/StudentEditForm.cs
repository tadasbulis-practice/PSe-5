using Lab10.Models;

namespace Lab10.Forms;

/// <summary>
/// Modal Add/Edit form. Uses constructor-driven validation: when the user
/// clicks OK we attempt to construct a Student or GraduateStudent — any
/// ArgumentException is shown in a MessageBox and the form stays open.
/// </summary>
public class StudentEditForm : Form
{
    // ── Input controls ─────────────────────────────────────────────
    private readonly NumericUpDown _id   = new() { Minimum = 1, Maximum = 1_000_000, Value = 1 };
    private readonly TextBox       _firstName = new();
    private readonly TextBox       _lastName  = new();
    private readonly TextBox       _email     = new();
    private readonly TextBox       _program   = new();
    private readonly NumericUpDown _year      = new() { Minimum = 2000, Maximum = 2100, Value = DateTime.Now.Year };
    private readonly CheckBox      _isGraduate = new() { Text = "Graduate student" };
    private readonly TextBox       _thesisTitle = new() { PlaceholderText = "Thesis title", Visible = false };

    /// <summary>The student created if the user pressed OK; null otherwise.</summary>
    public Student? Result { get; private set; }

    public StudentEditForm(Student? existing = null)
    {
        Text  = existing == null ? "Add student" : "Edit student";
        Width = 480; Height = 420;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        BuildLayout();

        if (existing != null) PopulateFromExisting(existing);

        _isGraduate.CheckedChanged += (_, _) =>
            _thesisTitle.Visible = _isGraduate.Checked;
    }

    private void BuildLayout()
    {
        const int labelLeft = 20;
        const int inputLeft = 160;
        const int rowHeight = 32;
        int top = 20;

        AddRow("Id:",            _id,         ref top, labelLeft, inputLeft, rowHeight);
        AddRow("First name:",    _firstName,  ref top, labelLeft, inputLeft, rowHeight);
        AddRow("Last name:",     _lastName,   ref top, labelLeft, inputLeft, rowHeight);
        AddRow("Email:",         _email,      ref top, labelLeft, inputLeft, rowHeight);
        AddRow("Study program:", _program,    ref top, labelLeft, inputLeft, rowHeight);
        AddRow("Enrollment year:", _year,     ref top, labelLeft, inputLeft, rowHeight);

        _isGraduate.Top = top; _isGraduate.Left = labelLeft; _isGraduate.Width = 140;
        _thesisTitle.Top = top; _thesisTitle.Left = inputLeft; _thesisTitle.Width = 280;
        Controls.Add(_isGraduate);
        Controls.Add(_thesisTitle);
        top += rowHeight + 10;

        var ok = new Button
        {
            Text = "Save", Top = top, Left = inputLeft, Width = 130,
            DialogResult = DialogResult.None,  // we set it manually after validation
        };
        var cancel = new Button
        {
            Text = "Cancel", Top = top, Left = inputLeft + 140, Width = 130,
            DialogResult = DialogResult.Cancel,
        };
        ok.Click += OkClicked;
        Controls.Add(ok);
        Controls.Add(cancel);

        AcceptButton = ok;
        CancelButton = cancel;
    }

    private void AddRow(string label, Control input, ref int top,
                        int labelLeft, int inputLeft, int rowHeight)
    {
        Controls.Add(new Label
        {
            Text = label, Top = top + 4, Left = labelLeft, Width = 130,
        });
        input.Top = top; input.Left = inputLeft; input.Width = 280;
        Controls.Add(input);
        top += rowHeight;
    }

    private void PopulateFromExisting(Student s)
    {
        _id.Value = s.Id;
        _id.Enabled = false;     // do not allow Id changes — Repository keys on it
        _firstName.Text = s.FirstName;
        _lastName.Text  = s.LastName;
        _email.Text     = s.Email;
        _program.Text   = s.StudyProgram;
        _year.Value     = s.EnrollmentYear;
        if (s is GraduateStudent g)
        {
            _isGraduate.Checked = true;
            _thesisTitle.Text   = g.ThesisTitle;
            _thesisTitle.Visible = true;
        }
    }

    private void OkClicked(object? sender, EventArgs e)
    {
        try
        {
            // Constructor performs validation — we just react to ArgumentException.
            if (_isGraduate.Checked)
            {
                Result = new GraduateStudent(
                    (int)_id.Value, _firstName.Text.Trim(), _lastName.Text.Trim(),
                    _email.Text.Trim(), _program.Text.Trim(), (int)_year.Value,
                    _thesisTitle.Text.Trim());
            }
            else
            {
                Result = new Student(
                    (int)_id.Value, _firstName.Text.Trim(), _lastName.Text.Trim(),
                    _email.Text.Trim(), _program.Text.Trim(), (int)_year.Value);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message,
                "Validation error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
