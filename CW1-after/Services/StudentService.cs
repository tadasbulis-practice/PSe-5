using CW1.Models;

namespace CW1.Services;

/// <summary>
/// Implements IStudentService.
/// All business logic lives here — no Console.* calls allowed.
/// </summary>
public class StudentService : IStudentService
{
    private readonly StudentRepository _repo;

    public StudentService(StudentRepository repo) => _repo = repo;

    // -------------------------------------------------------------------------
    // CRUD
    // -------------------------------------------------------------------------

    public List<Student> GetAll() => _repo.Students;

    public Student? GetById(int id) =>
        _repo.Students.FirstOrDefault(s => s.Id == id);

    public bool Add(Student student, out string error)
    {
        var errors = Validate(student);
        if (errors.Count > 0) { error = string.Join("; ", errors); return false; }
        if (_repo.Students.Any(s => s.Id == student.Id)) { error = "A student with this ID already exists."; return false; }
        _repo.Students.Add(student);
        error = "";
        return true;
    }

    public bool AddGrade(int studentId, int grade, out string error)
    {
        if (grade < 1 || grade > 10) { error = "Grade must be between 1 and 10."; return false; }
        var s = GetById(studentId);
        if (s == null) { error = "Student not found."; return false; }
        s.Grades.Add(grade);
        error = "";
        return true;
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    public double GetAverage(int studentId)
    {
        var s = GetById(studentId);
        if (s == null || s.Grades.Count == 0) return 0.0;
        return s.Grades.Sum() / (double)s.Grades.Count;
    }

    public List<string> Validate(Student s)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(s.Name))                   errors.Add("Name is empty");
        if (!s.Email.Contains('@') || !s.Email.Contains('.'))    errors.Add("Invalid email address");
        if (_repo.Groups.All(g => g.Code != s.GroupCode))        errors.Add("Group code does not exist");
        foreach (var g in s.Grades)
            if (g < 1 || g > 10) { errors.Add($"Grade {g} is out of range (must be 1-10)"); break; }
        return errors;
    }

    // -------------------------------------------------------------------------
    // Task 2 — LINQ versions
    // -------------------------------------------------------------------------

    public List<Student> Top3ByAverage_Linq() =>
        _repo.Students
            .OrderByDescending(s => s.Grades.Count == 0 ? 0.0 : s.Grades.Average())
            .Take(3)
            .ToList();

    public List<Student> StudentsInGroup_Linq(string groupCode) =>
        _repo.Students
            .Where(s => s.GroupCode == groupCode)
            .OrderBy(s => s.Name)
            .ToList();

    public (int Total, int TotalGrades, double MeanOfMeans, int MaxGrade, bool HasFailing, bool AllHaveEmail)
        Statistics_Linq()
    {
        return (
            Total:        _repo.Students.Count,
            TotalGrades:  _repo.Students.Sum(s => s.Grades.Count),
            MeanOfMeans:  _repo.Students.Average(s => s.Grades.Count == 0 ? 0.0 : s.Grades.Average()),
            MaxGrade:     _repo.Students.SelectMany(s => s.Grades).DefaultIfEmpty(0).Max(),
            HasFailing:   _repo.Students.Any(s => s.Grades.Any(g => g < 5)),
            AllHaveEmail: _repo.Students.All(s => !string.IsNullOrWhiteSpace(s.Email))
        );
    }

    // -------------------------------------------------------------------------
    // Task 2 — No-LINQ versions (only for/foreach/if — no LINQ operators)
    // -------------------------------------------------------------------------

    public List<Student> Top3ByAverage_Plain()
    {
        // Calculate average for each student manually
        var avgs = new List<(Student s, double avg)>();
        foreach (var s in _repo.Students)
        {
            double avg = 0.0;
            if (s.Grades.Count > 0)
            {
                int sum = 0;
                foreach (var g in s.Grades) sum += g;
                avg = sum / (double)s.Grades.Count;
            }
            avgs.Add((s, avg));
        }

        // Bubble sort descending by average
        for (int i = 0; i < avgs.Count - 1; i++)
            for (int j = i + 1; j < avgs.Count; j++)
                if (avgs[j].avg > avgs[i].avg)
                {
                    var tmp = avgs[i];
                    avgs[i] = avgs[j];
                    avgs[j] = tmp;
                }

        // Take the first 3
        var result = new List<Student>();
        for (int i = 0; i < avgs.Count && i < 3; i++)
            result.Add(avgs[i].s);
        return result;
    }

    public List<Student> StudentsInGroup_Plain(string groupCode)
    {
        // Filter by group code
        var filtered = new List<Student>();
        foreach (var s in _repo.Students)
            if (s.GroupCode == groupCode)
                filtered.Add(s);

        // Sort by name using insertion sort
        for (int i = 1; i < filtered.Count; i++)
        {
            var key = filtered[i];
            int j = i - 1;
            while (j >= 0 && string.Compare(filtered[j].Name, key.Name, StringComparison.Ordinal) > 0)
            {
                filtered[j + 1] = filtered[j];
                j--;
            }
            filtered[j + 1] = key;
        }
        return filtered;
    }

    public (int Total, int TotalGrades, double MeanOfMeans, int MaxGrade, bool HasFailing, bool AllHaveEmail)
        Statistics_Plain()
    {
        int total = 0;
        int totalGrades = 0;
        double sumOfMeans = 0.0;
        int maxGrade = 0;
        bool hasFailing = false;
        bool allHaveEmail = true;

        foreach (var s in _repo.Students)
        {
            total++;

            // Count grades and compute this student's mean
            int gradeSum = 0;
            int gradeCount = 0;
            foreach (var g in s.Grades)
            {
                gradeSum += g;
                gradeCount++;
                totalGrades++;
                if (g > maxGrade) maxGrade = g;
                if (g < 5) hasFailing = true;
            }
            if (gradeCount > 0)
                sumOfMeans += gradeSum / (double)gradeCount;

            if (string.IsNullOrWhiteSpace(s.Email)) allHaveEmail = false;
        }

        double meanOfMeans = total == 0 ? 0.0 : sumOfMeans / total;
        return (total, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }
}
