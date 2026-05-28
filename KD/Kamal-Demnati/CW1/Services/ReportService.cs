using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW1.Interfaces;
using CW1.Models;
using CW1.Services;

namespace CW1.Services
{


    public class ReportService
    {
        private readonly IStudentRepository _repository;
        private readonly AverageCalculator _calculator;

        public ReportService(
            IStudentRepository repository,
            AverageCalculator calculator)
        {
            _repository = repository;
            _calculator = calculator;
        }

        // =====================================================
        // TASK 2
        // LINQ VERSIONS
        // =====================================================

        public List<Student> GetTopByAverage(int n)
        {
            return _repository.GetStudents()
                .OrderByDescending(s => _calculator.Calculate(s))
                .Take(n)
                .ToList();
        }

        public List<Student> GetStudentsInGroupSortedByName(string code)
        {
            return _repository.GetStudents()
                .Where(s => s.GroupCode == code)
                .OrderBy(s => s.Name)
                .ToList();
        }

        public StatisticsResult GetStatistics()
        {
            var students = _repository.GetStudents();

            StatisticsResult result = new StatisticsResult();

            result.TotalStudents = students.Count;

            result.TotalGrades = students.Sum(s => s.Grades.Count);

            result.MeanAverage =
                students.Average(s => _calculator.Calculate(s));

            result.MaxGrade =
                students.SelectMany(s => s.Grades)
                        .DefaultIfEmpty(0)
                        .Max();

            result.HasFailing =
                students.Any(s => s.Grades.Any(g => g < 5));

            result.AllHaveEmail =
                students.All(s =>
                    !string.IsNullOrWhiteSpace(s.Email));

            return result;
        }

        // =====================================================
        // TASK 2
        // WITHOUT LINQ VERSIONS
        // =====================================================

        public List<Student> GetTopByAverageWithoutLinq(int n)
        {
            List<Student> students = new();

            foreach (Student s in _repository.GetStudents())
            {
                students.Add(s);
            }

            students.Sort((a, b) =>
            {
                double avgA = _calculator.Calculate(a);
                double avgB = _calculator.Calculate(b);

                return avgB.CompareTo(avgA);
            });

            List<Student> result = new();

            for (int i = 0; i < n && i < students.Count; i++)
            {
                result.Add(students[i]);
            }

            return result;
        }

        public List<Student> GetStudentsInGroupSortedByNameWithoutLinq(string code)
        {
            List<Student> result = new();

            foreach (Student s in _repository.GetStudents())
            {
                if (s.GroupCode == code)
                {
                    result.Add(s);
                }
            }

            result.Sort((a, b) =>
                a.Name.CompareTo(b.Name));

            return result;
        }

        public StatisticsResult GetStatisticsWithoutLinq()
        {
            List<Student> students = _repository.GetStudents();

            StatisticsResult result = new();

            int totalStudents = students.Count;

            int totalGrades = 0;

            double averageSum = 0;

            int maxGrade = 0;

            bool hasFailing = false;

            bool allHaveEmail = true;

            foreach (Student student in students)
            {
                totalGrades += student.Grades.Count;

                double avg = _calculator.Calculate(student);

                averageSum += avg;

                if (string.IsNullOrWhiteSpace(student.Email))
                {
                    allHaveEmail = false;
                }

                foreach (int grade in student.Grades)
                {
                    if (grade > maxGrade)
                    {
                        maxGrade = grade;
                    }

                    if (grade < 5)
                    {
                        hasFailing = true;
                    }
                }
            }

            double meanAverage = 0;

            if (totalStudents > 0)
            {
                meanAverage = averageSum / totalStudents;
            }

            result.TotalStudents = totalStudents;
            result.TotalGrades = totalGrades;
            result.MeanAverage = meanAverage;
            result.MaxGrade = maxGrade;
            result.HasFailing = hasFailing;
            result.AllHaveEmail = allHaveEmail;

            return result;
        }
    }
}
