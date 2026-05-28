using CW1After.Interfaces;
using CW1After.Models;
using System.Collections.Generic;


namespace CW1After.Services;
public class StudentValidator
{
    private readonly IStudentRepository _repo;

    public StudentValidator(IStudentRepository repo)
    {
        _repo = repo;
    }

    public List<string> Validate(Student s)
    {
        var errors = new List<string>();
        if (s == null) { errors.Add("Student is null"); return errors; }
        if (string.IsNullOrWhiteSpace(s.Name)) errors.Add("Name empty");
        if (string.IsNullOrWhiteSpace(s.Email) || !s.Email.Contains('@') || !s.Email.Contains('.')) errors.Add("Bad email");

        bool found = false;
        foreach (var g in _repo.GetGroups())
        {
            if (g.Code == s.GroupCode) { found = true; break; }
        }
        if (!found) errors.Add("Unknown group");

        foreach (var gr in s.Grades)
        {
            if (gr < 1 || gr > 10) { errors.Add($"Grade {gr} out of range"); break; }
        }

        return errors;
    }
}