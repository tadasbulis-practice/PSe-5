using System;
using System.Linq;
using JohnBastille.Lab_5.Models;
using JohnBastille.Lab_5.Interfaces;

namespace Lab_5.Services;

public class PartialNameFinder : IStudentFinder
{
    public Student? Find(List<Student> students, string query)
    {
        // Defensive: don't attempt to search if query is null/whitespace
        if (string.IsNullOrWhiteSpace(query))
            return null;

        return students.FirstOrDefault(s =>
            // Guard against null student or null/empty name
            !string.IsNullOrEmpty(s?.Name) &&
            s.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
    }
}