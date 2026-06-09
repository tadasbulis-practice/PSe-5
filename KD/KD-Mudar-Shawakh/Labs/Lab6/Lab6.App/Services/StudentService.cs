using System;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IStudentPrinter _printer;
        private readonly IAverageStrategy _averageStrategy;
        private readonly IStudentValidator _validator;

        // Constructor Injection for ALL four dependencies.
        // The service depends on INTERFACES only — it has no idea which
        // concrete implementations Program.cs decided to plug in.
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

        // --- Single-student operations (delegate to repository / validator) ---

        public void AddStudent(Student student)
        {
            // Validate FIRST — invalid students never enter the container.
            if (_validator.Validate(student))
            {
                _repository.Add(student);
            }
            else
            {
                Console.WriteLine($"[Rejected] {student.Name} failed validation.");
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

        // --- Collection operations (the LAB-6 additions) ---

        public void PrintAllStudents()
        {
            var students = _repository.GetAll();
            _printer.PrintStudents(students);
        }

        public double CalculateGroupAverage()
        {
            // Filter to valid students FIRST, then calculate.
            // This is why IStudentValidator.ValidateAll returns a list — it lets us
            // chain validation into the calculation without writing a loop here.
            var validStudents = _validator.ValidateAll(_repository.GetAll());
            return _averageStrategy.Calculate(validStudents);
        }
    }
}