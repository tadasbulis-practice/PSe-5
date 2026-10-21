using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Lab10.Views;

/// <summary>
/// Tiny WinForms-style MessageBox replacement for Avalonia.
/// Avalonia core has no MessageBox; instead of pulling a NuGet package
/// we build one ourselves — which is a nice teaching moment:
/// an "alert dialog" is just a small Window with text and buttons.
///
///     await MessageDialog.ShowAsync(this, "Saved!", "All good", DialogIcon.Info);
///     var r = await MessageDialog.ShowAsync(this, "Delete?", "Sure?",
///                                           DialogIcon.Question, DialogButtons.YesNo);
/// </summary>
public enum DialogIcon    { Info, Warning, Error, Question }
public enum DialogButtons { Ok, YesNo, OkCancel }
public enum DialogResult  { Ok, Cancel, Yes, No }

public static class MessageDialog
{
    public static Task<DialogResult> ShowAsync(
        Window owner,
        string title,
        string text,
        DialogIcon icon       = DialogIcon.Info,
        DialogButtons buttons = DialogButtons.Ok)
    {
        var dlg = new Window
        {
            Title  = title,
            Width  = 420,
            MinHeight = 160,
            SizeToContent = SizeToContent.Height,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            ShowInTaskbar = false,
        };

        // ── Top row: icon glyph + message text ──────────────────
        var glyph = new TextBlock
        {
            Text       = IconGlyph(icon),
            FontSize   = 28,
            Foreground = IconBrush(icon),
            VerticalAlignment   = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin     = new Thickness(0, 0, 12, 0),
        };
        var msg = new TextBlock
        {
            Text         = text,
            TextWrapping = TextWrapping.Wrap,
            VerticalAlignment = VerticalAlignment.Center,
        };

        var topRow = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin      = new Thickness(20, 20, 20, 12),
        };
        topRow.Children.Add(glyph);
        topRow.Children.Add(msg);

        // ── Bottom row: buttons ─────────────────────────────────
        var buttonRow = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing     = 8,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin      = new Thickness(20, 0, 20, 16),
        };

        var tcs = new TaskCompletionSource<DialogResult>();
        void Close(DialogResult r) { tcs.TrySetResult(r); dlg.Close(); }

        switch (buttons)
        {
            case DialogButtons.Ok:
                buttonRow.Children.Add(MakeBtn("OK",     () => Close(DialogResult.Ok),     true));
                break;
            case DialogButtons.OkCancel:
                buttonRow.Children.Add(MakeBtn("OK",     () => Close(DialogResult.Ok),     true));
                buttonRow.Children.Add(MakeBtn("Cancel", () => Close(DialogResult.Cancel), false));
                break;
            case DialogButtons.YesNo:
                buttonRow.Children.Add(MakeBtn("Yes",    () => Close(DialogResult.Yes),    true));
                buttonRow.Children.Add(MakeBtn("No",     () => Close(DialogResult.No),     false));
                break;
        }

        var root = new DockPanel { LastChildFill = true };
        DockPanel.SetDock(buttonRow, Dock.Bottom);
        root.Children.Add(buttonRow);
        root.Children.Add(topRow);
        dlg.Content = root;

        // If user closes via window-X, treat it as Cancel/No
        dlg.Closing += (_, _) =>
        {
            if (!tcs.Task.IsCompleted)
                tcs.TrySetResult(buttons == DialogButtons.YesNo ? DialogResult.No : DialogResult.Cancel);
        };

        _ = dlg.ShowDialog(owner);
        return tcs.Task;
    }

    private static Button MakeBtn(string text, Action onClick, bool isDefault)
    {
        var b = new Button
        {
            Content   = text,
            Padding   = new Thickness(20, 4),
            MinWidth  = 80,
            IsDefault = isDefault,
        };
        b.Click += (_, _) => onClick();
        return b;
    }

    private static string IconGlyph(DialogIcon i) => i switch
    {
        DialogIcon.Info     => "i",
        DialogIcon.Warning  => "!",
        DialogIcon.Error    => "x",
        DialogIcon.Question => "?",
        _ => "i",
    };

    private static IBrush IconBrush(DialogIcon i) => i switch
    {
        DialogIcon.Info     => new SolidColorBrush(Color.Parse("#1976D2")),
        DialogIcon.Warning  => new SolidColorBrush(Color.Parse("#F57C00")),
        DialogIcon.Error    => new SolidColorBrush(Color.Parse("#C62828")),
        DialogIcon.Question => new SolidColorBrush(Color.Parse("#0097A7")),
        _ => new SolidColorBrush(Colors.Black),
    };
}
