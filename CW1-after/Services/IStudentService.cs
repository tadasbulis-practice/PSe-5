using CW1.Models;

namespace CW1.Services;

/// <summary>
/// Contract for the student service layer.
/// The UI layer depends only on this interface, never on the concrete class.
/// </summary>
public interface IStudentService
{
    // --- Basic operations ---
    List<Student> GetAll();
    Student? GetById(int id);
    bool Add(Student student, out string error);
    bool AddGrade(int studentId, int grade, out string error);

    // --- Queries ---
    double GetAverage(int studentId);
    List<string> Validate(Student student);

    // --- Task 2: LINQ versions ---
    List<Student> Top3ByAverage_Linq();
    List<Student> StudentsInGroup_Linq(string groupCode);
    (int Total, int TotalGrades, double MeanOfMeans, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_Linq();

    // --- Task 2: No-LINQ versions (for/foreach/if only) ---
    List<Student> Top3ByAverage_Plain();
    List<Student> StudentsInGroup_Plain(string groupCode);
    (int Total, int TotalGrades, double MeanOfMeans, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_Plain();
}
