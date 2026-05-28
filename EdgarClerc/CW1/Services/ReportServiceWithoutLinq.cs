using CW1.Interface;
using CW1.Models;

namespace CW1.Services;

public class ReportServiceWithoutLinq : IReportService
{
    private readonly IStudentRepository _repository;
    private readonly AverageStategy _averageStategy;

    public ReportServiceWithoutLinq(IStudentRepository repository, AverageStategy averageStategy)
    {
        _repository = repository;
        _averageStategy = averageStategy;
    }

    public List<(Student student, double avg)> GetTopByAverage(int numberOfStudents = 3)
    {
        IReadOnlyList<Student> students = _repository.FindAll();
        var tupleList = new List<(Student student, double avg)>();

        // 1. Manually project/map to tuples with averages
        foreach (var s in students)
        {
            tupleList.Add((s, _averageStategy.GetAverage(s)));
        }

        // 2. Custom Bubble Sort to order by average descending
        for (int i = 0; i < tupleList.Count - 1; i++)
        {
            for (int j = 0; j < tupleList.Count - i - 1; j++)
            {
                if (tupleList[j].avg < tupleList[j + 1].avg)
                {
                    var temp = tupleList[j];
                    tupleList[j] = tupleList[j + 1];
                    tupleList[j + 1] = temp;
                }
            }
        }

        // 3. Take the top requested number of students manually
        var result = new List<(Student student, double avg)>();
        int countToTake = tupleList.Count < numberOfStudents ? tupleList.Count : numberOfStudents;
        for (int i = 0; i < countToTake; i++)
        {
            result.Add(tupleList[i]);
        }

        return result;
    }

    public List<Student> GetStudentsInGroupSortedByName(string gc)
    {
        IReadOnlyList<Student> students = _repository.FindAll();
        var filteredStudents = new List<Student>();

        // 1. Filter out by group code
        foreach (var s in students)
        {
            if (s.GroupCode == gc)
            {
                filteredStudents.Add(s);
            }
        }

        // 2. Sort alphabetically by Name using String.Compare
        for (int i = 0; i < filteredStudents.Count - 1; i++)
        {
            for (int j = 0; j < filteredStudents.Count - i - 1; j++)
            {
                if (
                    string.Compare(
                        filteredStudents[j].Name,
                        filteredStudents[j + 1].Name,
                        StringComparison.Ordinal
                    ) > 0
                )
                {
                    var temp = filteredStudents[j];
                    filteredStudents[j] = filteredStudents[j + 1];
                    filteredStudents[j + 1] = temp;
                }
            }
        }

        return filteredStudents;
    }

    public (
        int totalStudents,
        int totalGrades,
        double meanOfMeans,
        int maxGrade,
        bool hasFailing,
        bool allHaveEmail
    ) GetStatistics()
    {
        IReadOnlyList<Student> students = _repository.FindAll();

        int totalStudents = students.Count;
        int totalGrades = 0;
        int maxGrade = 0;
        bool hasFailing = false;
        bool allHaveEmail = true;

        double sumOfStudentAverages = 0;
        int studentsWithGradesCount = 0;

        foreach (var s in students)
        {
            // Count total grades
            totalGrades += s.Grades.Count;

            // Check if any student has missing/empty email strings
            if (string.IsNullOrWhiteSpace(s.Email))
            {
                allHaveEmail = false;
            }

            // Calculate student average manually for Mean of Means
            if (s.Grades.Count > 0)
            {
                int studentGradeSum = 0;
                foreach (var g in s.Grades)
                {
                    studentGradeSum += g;

                    // Track if any grade is a failing grade (< 5)
                    if (g < 5)
                    {
                        hasFailing = true;
                    }

                    // Track maximum overall grade found
                    if (g > maxGrade)
                    {
                        maxGrade = g;
                    }
                }

                sumOfStudentAverages += (double)studentGradeSum / s.Grades.Count;
                studentsWithGradesCount++;
            }
        }

        // Mean of Means fallback calculation matching your LINQ logic
        double meanOfMeans = totalStudents == 0 ? 0.0 : (sumOfStudentAverages / totalStudents);

        return (totalStudents, totalGrades, meanOfMeans, maxGrade, hasFailing, allHaveEmail);
    }
}
