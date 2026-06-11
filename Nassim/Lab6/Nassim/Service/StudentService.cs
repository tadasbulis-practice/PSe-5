using System;
using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IStudentPrinter _printer;
        private readonly IAverageStrategy _averageStrategy;
        private readonly IStudentValidator _validator;

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

        public void AddStudent(Student student) => _repository.Add(student);

        public void PrintAllStudents()
        {
            var students = _repository.GetAll();
            _printer.PrintStudents(students);
        }

        public void PrintValidStudents()
        {
            var students = _repository.GetAll();
            var valid = _validator.ValidateAll(students);
            Console.WriteLine($"Valid students: {valid.Count}/{students.Count}");
            _printer.PrintStudents(valid);
        }

        public double CalculateGroupAverage()
        {
            var students = _repository.GetAll();
            var valid = _validator.ValidateAll(students);
            return _averageStrategy.Calculate(valid);
        }
    }
}