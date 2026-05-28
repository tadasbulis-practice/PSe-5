using System.Collections.Generic;

using CW1After.Models;



namespace CW1After.Interfaces;
public interface IStudentService
{
    IEnumerable<Student> ListAll();
    (bool Success, List<string> Errors) AddStudent(Student s);
    bool AddGrade(int studentId, int grade);
    Student? FindById(int id);
    double GetAverage(Student s);

    // report variants can live in ReportService, but exposing here is ok if you prefer
    IEnumerable<(Student Student, double Avg)> Top3ByAverage_Linq();
    IEnumerable<(Student Student, double Avg)> Top3ByAverage_NoLinq();
    IEnumerable<Student> StudentsInGroup_Linq(string group);
    IEnumerable<Student> StudentsInGroup_NoLinq(string group);

    // statistics (both variants)
    (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_Linq();
    (int TotalStudents, int TotalGrades, double MeanOfAverages, int MaxGrade, bool HasFailing, bool AllHaveEmail) Statistics_NoLinq();
}
    
