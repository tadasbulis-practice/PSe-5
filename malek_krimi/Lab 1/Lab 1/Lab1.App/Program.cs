using System;

namespace Lab1.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Group group = new Group("OOP Group A");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1 - Add student");
                Console.WriteLine("2 - Show students");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudentMenu(group);
                        break;

                    case "2":
                        group.PrintAll();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AddStudentMenu(Group group)
        {
            Student student = new Student();

            Console.Write("Enter ID: ");
            student.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            student.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            student.LastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            student.Email = Console.ReadLine();

            group.AddStudent(student);
            Console.WriteLine("Student added successfully.");
        }
    }
}
