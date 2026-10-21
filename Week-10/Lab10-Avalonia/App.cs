using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Lab10.Interfaces;
using Lab10.Views;

namespace Lab10;

/// <summary>
/// Avalonia Application. Roughly the equivalent of WinForms'
/// ApplicationConfiguration.Initialize(); + Application.Run(form).
///
/// We do this code-only (no .axaml file) to keep the comparison with
/// the WinForms version line-for-line. In a real Avalonia project you
/// would normally use an App.axaml + MainWindow.axaml pair.
/// </summary>
public class App : Application
{
    /// <summary>Set by Program.Main before the GUI starts.</summary>
    public static IStudentRepository Repository { get; set; } = null!;

    public override void Initialize()
    {
        // Pick the Fluent theme — looks native-ish on Win/Mac/Linux.
        Styles.Add(new FluentTheme());

        // DataGrid lives in a separate package and ships its own theme:
        Styles.Add(new StyleInclude(new Uri("avares://Lab10/"))
        {
            Source = new Uri(
                "avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml")
        });
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow(Repository);
        }
        base.OnFrameworkInitializationCompleted();
    }
}
