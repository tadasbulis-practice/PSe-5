using Lab10.Models;

namespace Lab10.Interfaces;

/// <summary>
/// Save/Load students to a file. JSON and CSV implementations satisfy this
/// contract. The Form depends only on this interface (Strategy pattern, OCP).
/// All file errors are surfaced as IOException so the UI has one type to catch.
/// </summary>
public interface IStudentFileService
{
    /// <summary>Format-specific extension (e.g. ".json", ".csv") — used by the dialog filter.</summary>
    string FileExtension { get; }

    /// <summary>Display label for the dialog filter (e.g. "JSON files").</summary>
    string DisplayLabel { get; }

    /// <summary>Persist students to <paramref name="path"/>. Throws IOException on failure.</summary>
    void Save(IReadOnlyList<Student> students, string path);

    /// <summary>Load students from <paramref name="path"/>. Throws IOException on failure.</summary>
    List<Student> Load(string path);
}
