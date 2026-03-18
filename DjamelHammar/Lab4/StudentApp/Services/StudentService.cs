using System;
using System.Collections.Generic;
using StudentApp.Interfaces;
using StudentApp.Models;

namespace StudentApp.Services
{
    public class StudentService
    {
        private readonly IStudentPrinter _printer;
        private readonly IAverageStrategy _average;
        private readonly IStudentValidator _validator;
        private readonly IStudentFinder _finder;

        public StudentService(IStudentPrinter printer, IAverageStrategy average, IStudentValidator validator, IStudentFinder finder)
        {
            _printer = printer;
            _average = average;
            _validator = validator;
            _finder = finder;
        }

        public void Run(Group group)
        {
            foreach (var student in group.Students)
            {
                var avg = _average.Calculate(student.Grades);
                var isValid = _validator.Validate(avg);
                _printer.PrintStudent(student.Name, avg, isValid);
            }
        }
    }
}