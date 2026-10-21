using Avalonia;
using Lab10.Exceptions;
using Lab10.Implementations.Repository;
using Lab10.Interfaces;

namespace Lab10;

internal static class Program
{
    /// <summary>
    /// COMPOSITION ROOT — same as the WinForms version.
    /// 1) Decide which IStudentRepository to use (API or in-memory).
    /// 2) Hand it to App via a static slot so MainWindow can read it on startup.
    /// 3) Start the Avalonia application loop.
    /// </summary>
    [STAThread]                                       // [STAThread] still required on Windows
    public static void Main(string[] args)
    {
        const string apiUrl = "http://localhost:6001";
        const bool   useApi = false;                  // ← flip to true when Docker API is up

        IStudentRepository repository;
        try
        {
            repository = useApi
                ? new ApiStudentRepository(apiUrl)
                : new MemoryStudentRepository();
        }
        catch (RepositoryException)
        {
            // Same fallback strategy as Lab-9 / WinForms version
            repository = new MemoryStudentRepository();
        }

        App.Repository = repository;

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    /// <summary>
    /// Avalonia bootstrap. UsePlatformDetect() picks Win32 / X11 / Cocoa automatically.
    /// </summary>
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
