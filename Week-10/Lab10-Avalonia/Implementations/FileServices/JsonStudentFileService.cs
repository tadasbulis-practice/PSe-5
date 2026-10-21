using System.Text.Json;
using System.Text.Json.Serialization;
using Lab10.Interfaces;
using Lab10.Models;

namespace Lab10.Implementations.FileServices;

/// <summary>
/// Saves/loads students to a JSON file using System.Text.Json.
///
/// Polymorphism note: JSON keeps Student vs GraduateStudent apart via a
/// "$type" discriminator. We register both types using JsonDerivedType so
/// reading back returns the correct concrete subclass.
/// </summary>
public class JsonStudentFileService : IStudentFileService
{
    public string FileExtension => ".json";
    public string DisplayLabel  => "JSON files";

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolverWithDerivedTypes(),
    };

    public void Save(IReadOnlyList<Student> students, string path)
    {
        try
        {
            var json = JsonSerializer.Serialize(students, Options);
            File.WriteAllText(path, json);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new IOException(
                $"No permission to write to '{path}'.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new IOException(
                $"The folder for '{path}' does not exist.", ex);
        }
        catch (IOException) { throw; }
    }

    public List<Student> Load(string path)
    {
        try
        {
            var json = File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<List<Student>>(json, Options);
            return list ?? new List<Student>();
        }
        catch (FileNotFoundException ex)
        {
            throw new IOException($"File not found: '{path}'.", ex);
        }
        catch (JsonException ex)
        {
            throw new IOException(
                $"The file '{path}' is not valid JSON: {ex.Message}", ex);
        }
        catch (ArgumentException ex)
        {
            // bubbled up from Student constructor validation
            throw new IOException(
                $"A student record in '{path}' has invalid data: {ex.Message}", ex);
        }
        catch (IOException) { throw; }
    }
}

/// <summary>
/// Type-info resolver that registers GraduateStudent as a derived type of Student.
/// Without this, deserializing a List&lt;Student&gt; can't recover graduate records.
/// </summary>
internal class DefaultJsonTypeInfoResolverWithDerivedTypes
    : System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver
{
    public override System.Text.Json.Serialization.Metadata.JsonTypeInfo GetTypeInfo(
        Type type, JsonSerializerOptions options)
    {
        var info = base.GetTypeInfo(type, options);
        if (type == typeof(Student))
        {
            info.PolymorphismOptions = new System.Text.Json.Serialization.Metadata.JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling =
                    JsonUnknownDerivedTypeHandling.FailSerialization,
            };
            info.PolymorphismOptions.DerivedTypes.Add(
                new System.Text.Json.Serialization.Metadata.JsonDerivedType(
                    typeof(Student), "Student"));
            info.PolymorphismOptions.DerivedTypes.Add(
                new System.Text.Json.Serialization.Metadata.JsonDerivedType(
                    typeof(GraduateStudent), "GraduateStudent"));
        }
        return info;
    }
}
