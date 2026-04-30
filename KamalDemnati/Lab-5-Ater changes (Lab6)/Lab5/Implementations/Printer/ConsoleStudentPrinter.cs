
using Lab5.Interfaces;
using Lab5.Models;

namespace Lab5.Implementations.Printer;

using System;
using System.Collections.Generic;

public class ConsoleStudentPrinter : IStudentPrinter
{
    public void PrintStudents(List<Student> students)
    {
        foreach (var s in students)
        {
            Console.WriteLine($"ID: {s.Id}, Name: {s.Name}, Grade: {s.Grade}, Weight: {s.Weight}");
        }
    }
}