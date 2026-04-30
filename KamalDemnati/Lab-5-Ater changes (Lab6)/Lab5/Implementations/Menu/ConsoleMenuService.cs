using Lab5.Interfaces;
using Lab5.Models;
using Lab5.Services;
namespace Lab5.Implementations.Menu;

using Lab5.Implementations.Strategy;
using System;

public class ConsoleMenuService : IMenuService
{
    private readonly StudentService _studentService;
    private int _idCounter = 1;

    public ConsoleMenuService(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    _studentService.PrintAllStudents();
                    break;

                case "3":
                    CalculateAverageWithChoice();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("==== MENU ====");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Print all students");
        Console.WriteLine("3. Calculate group average");
        Console.WriteLine("0. Exit");
    }

    private void AddStudent()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter grade: ");
        if (!double.TryParse(Console.ReadLine(), out double grade))
        {
            Console.WriteLine("Invalid grade.");
            return;
        }

        Console.Write("Enter weight (optional, press Enter for 1): ");
        string weightInput = Console.ReadLine();
        double weight = 1;

        if (!string.IsNullOrWhiteSpace(weightInput))
        {
            if (!double.TryParse(weightInput, out weight))
            {
                Console.WriteLine("Invalid weight.");
                return;
            }
        }



        var student = new Student(_idCounter++, name, grade, weight);

        _studentService.AddStudent(student);
        Console.WriteLine("Student added.");


    }

    private void CalculateAverageWithChoice()
    {
        Console.WriteLine("Choose average calculation method:");
        Console.WriteLine("1 - Simple average");
        Console.WriteLine("2 - Median");
        Console.WriteLine("3 - Weighted");

        var choice = Console.ReadLine();
        IAverageStrategy strategy;

        switch (choice)
        {
            case "1":
                strategy = new SimpleAverageStrategy();
                break;

            case "2":
                strategy = new MedianAverageStrategy();
                break;

            case "3":
                strategy = new WeightedAverageStrategy();
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        var avg = _studentService.CalculateGroupAverage(strategy);
        Console.WriteLine($"Group average: {avg:F2}");
    }
}