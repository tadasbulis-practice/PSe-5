using System.Globalization;
using Lab10.Interfaces;
using Lab10.Models;

namespace Lab10.Implementations.FileServices;

/// <summary>
/// Manual CSV save/load — no NuGet dependency, on purpose.
/// Fields are separated by ';' to be Excel-friendly in EU locales.
/// A "Type" column distinguishes Student rows from GraduateStudent rows so the
/// inheritance hierarchy round-trips through the file.
/// </summary>
public class CsvStudentFileService : IStudentFileService
{
    public string FileExtension => ".csv";
    public string DisplayLabel  => "CSV files";

    private const char Sep = ';';
    private static readonly string Header =
        "Id;FirstName;LastName;Email;StudyProgram;EnrollmentYear;Type;ThesisTitle";

    public void Save(IReadOnlyList<Student> students, string path)
    {
        try
        {
            using var w = new StreamWriter(path);
            w.WriteLine(Header);
            foreach (var s in students)
            {
                var (type, thesis) = s is GraduateStudent g
                    ? ("GRAD", Escape(g.ThesisTitle))
                    : ("STUD", "");

                w.WriteLine(string.Join(Sep,
                    s.Id,
                    Escape(s.FirstName),
                    Escape(s.LastName),
                    Escape(s.Email),
                    Escape(s.StudyProgram),
                    s.EnrollmentYear.ToString(CultureInfo.InvariantCulture),
                    type,
                    thesis));
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new IOException($"No permission to write to '{path}'.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new IOException($"The folder for '{path}' does not exist.", ex);
        }
        catch (IOException) { throw; }
    }

    public List<Student> Load(string path)
    {
        var list = new List<Student>();
        try
        {
            using var r = new StreamReader(path);
            r.ReadLine();   // skip header
            string? line;
            int lineNumber = 1;
            while ((line = r.ReadLine()) != null)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(Sep);
                if (parts.Length < 7)
                    throw new IOException($"Line {lineNumber}: not enough columns.");

                try
                {
                    var id   = int.Parse(parts[0], CultureInfo.InvariantCulture);
                    var year = int.Parse(parts[5], CultureInfo.InvariantCulture);
                    var type = parts[6];

                    Student s = type == "GRAD"
                        ? new GraduateStudent(id, Unescape(parts[1]), Unescape(parts[2]),
                              Unescape(parts[3]), Unescape(parts[4]), year,
                              Unescape(parts.Length > 7 ? parts[7] : ""))
                        : new Student(id, Unescape(parts[1]), Unescape(parts[2]),
                              Unescape(parts[3]), Unescape(parts[4]), year);

                    list.Add(s);
                }
                catch (FormatException ex)
                {
                    throw new IOException(
                        $"Line {lineNumber}: invalid number format ({ex.Message}).", ex);
                }
                catch (ArgumentException ex)
                {
                    throw new IOException(
                        $"Line {lineNumber}: {ex.Message}", ex);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            throw new IOException($"File not found: '{path}'.", ex);
        }
        catch (IOException) { throw; }
        return list;
    }

    // Minimal CSV escape: replace separator and newline characters.
    private static string Escape(string s) =>
        s.Replace(";", ",").Replace("\n", " ").Replace("\r", " ");

    private static string Unescape(string s) => s;
}
