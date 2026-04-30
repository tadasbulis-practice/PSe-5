using Lab10.Exceptions;
using Lab10.Forms;
using Lab10.Implementations.Repository;
using Lab10.Interfaces;

namespace Lab10;

internal static class Program
{
    /// <summary>
    /// COMPOSITION ROOT — pick the repository (API or Memory) and start the form.
    /// Same pattern as Lab-9, just MessageBox replaces Console.WriteLine for errors.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        const string apiUrl = "http://localhost:6001";
        const bool   useApi = false;     // ← set true once the Docker API is running

        IStudentRepository repository;
        if (useApi)
        {
            try
            {
                repository = new ApiStudentRepository(apiUrl);
            }
            catch (RepositoryException ex)
            {
                MessageBox.Show(
                    $"API is unavailable:\n{ex.Message}\n\n" +
                    $"Falling back to in-memory data.\n\n" +
                    $"Inner: {ex.InnerException?.Message}",
                    "API unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                repository = new MemoryStudentRepository();
            }
        }
        else
        {
            repository = new MemoryStudentRepository();
        }

        Application.Run(new MainForm(repository));
    }
}
