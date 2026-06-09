using System;
using Lab6.App.Interfaces;
using Lab6.App.Models;

namespace Lab6.App.Services
{
    public class ConsoleMenuService : IMenuService
    {
        private readonly StudentService _service;

        public ConsoleMenuService(StudentService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("===== LAB-6: Containers in OOP =====");
            Console.WriteLine("Press Enter on any prompt to accept the default.\n");

            // --- TASK 1: Add multiple students ---
            Console.WriteLine("--- Step 1: Adding students ---");

            _service.AddStudent(BuildStudent(1, "Alice", "alice@test.com", new[] { 9, 8 }));
            _service.AddStudent(BuildStudent(2, "Bob", "bob@test.com", new[] { 7, 10 }));
            _service.AddStudent(BuildStudent(3, "Carol", "carol@test.com", new[] { 6, 7, 8 }));

            // One invalid student on purpose, to demonstrate the validator filtering.
            _service.AddStudent(BuildStudent(99, "Broken", "no-at-sign", new[] { 5 }));

            // --- TASK 2: Retrieve all students ---
            Console.WriteLine("\n--- Step 2: Retrieving all students ---");
            _service.PrintAllStudents();

            // --- TASK 3: Find by ID (demonstrates GetById) ---
            Console.WriteLine("\n--- Step 3: Find by ID ---");
            int searchId = AskInt("Enter student ID to look up", 2);
            var found = _service.FindStudentById(searchId);
            Console.WriteLine(found == null
                ? $"No student with ID {searchId}."
                : $"Found: {found.Name} ({found.Email})");

            // --- TASK 4 + 5: Validate and calculate group average ---
            Console.WriteLine("\n--- Step 4: Group average (valid students only) ---");
            double groupAverage = _service.CalculateGroupAverage();
            Console.WriteLine($"Group average = {groupAverage:F2}");

            // --- TASK 6: Remove and re-print to confirm the container is mutable ---
            Console.WriteLine("\n--- Step 5: Removing a student ---");
            int removeId = AskInt("Enter student ID to remove", 1);
            bool removed = _service.RemoveStudent(removeId);
            Console.WriteLine(removed
                ? $"Removed student with ID {removeId}."
                : $"No student with ID {removeId} — nothing removed.");

            Console.WriteLine("\n--- Step 6: Final state ---");
            _service.PrintAllStudents();

            Console.WriteLine("\n===== Done =====");
        }

        // --- Private helpers — UI plumbing only, no business logic. ---

        private static Student BuildStudent(int id, string name, string email, int[] grades)
        {
            var s = new Student(id, name, email);
            foreach (var g in grades)
            {
                s.AddGrade(g);
            }
            return s;
        }

        private static int AskInt(string prompt, int defaultValue)
        {
            Console.Write($"{prompt} [{defaultValue}]: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine($"Invalid input. Using default ({defaultValue}).");
            return defaultValue;
        }
    }
}