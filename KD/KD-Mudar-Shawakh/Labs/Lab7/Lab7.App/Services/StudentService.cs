using System;
using System.Collections.Generic;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IStudentPrinter _printer;
        private readonly IAverageStrategy _averageStrategy;
        private readonly IStudentValidator _validator;

        // Constructor Injection — depends on INTERFACES only.
        // Has no idea which concrete implementations Program.cs picked.
        public StudentService(
            IStudentRepository repository,
            IStudentPrinter printer,
            IAverageStrategy averageStrategy,
            IStudentValidator validator)
        {
            _repository = repository;
            _printer = printer;
            _averageStrategy = averageStrategy;
            _validator = validator;
        }

        // --- Student-level operations ---

        public void AddStudent(Student student)
        {
            if (_validator.Validate(student))
            {
                _repository.Add(student);
            }
            else
            {
                Console.WriteLine($"  [Rejected] {student.FullName} failed validation.");
            }
        }

        public Student? FindStudentById(int id)
        {
            return _repository.GetById(id);
        }

        public bool RemoveStudent(int id)
        {
            return _repository.Remove(id);
        }

        public void PrintAllStudents()
        {
            _printer.PrintStudents(_repository.GetAll());
        }

        // --- Group / Faculty operations (NEW in LAB-7) ---

        public IReadOnlyList<Group> GetAllGroups()
        {
            return _repository.GetAllGroups();
        }

        public void PrintGroup(string code)
        {
            var group = _repository.GetGroupByCode(code);
            if (group == null)
            {
                Console.WriteLine($"  No group with code '{code}'.");
                return;
            }

            Console.WriteLine($"\n  Group [{group.Code}] — {group.StudyProgram} ({group.EnrollmentYear})");
            _printer.PrintStudents(group.Students);
        }

        public void PrintFaculty()
        {
            var faculty = _repository.GetFaculty();
            Console.WriteLine($"\n  Faculty: {faculty.Name}");
            Console.WriteLine($"  Groups: {faculty.Groups.Count}");

            foreach (var g in faculty.Groups)
            {
                Console.WriteLine($"    [{g.Code,-8}]  {g.StudyProgram,-25} {g.EnrollmentYear}  — {g.Students.Count} students");
            }
        }

        // --- Calculation ---

        public double CalculateGroupAverage()
        {
            // Filter to valid students first — invalid ones don't pollute the average.
            // LAB-7's IStudentValidator only has Validate(single), so we loop here.
            var allStudents = _repository.GetAll();
            var valid = new List<Student>();

            foreach (var s in allStudents)
            {
                if (_validator.Validate(s))
                {
                    valid.Add(s);
                }
            }

            return _averageStrategy.Calculate(valid);
        }
    }
}