using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Group group = Seed();

        IStudentPrinter printer = new DetailedStudentPrinter();
        IAverageStrategy average = new WeightedAverageStrategy();
        IStudentFinder finder = new FindById();
        IStudentValidator validator = new StrictValidator();

        IMenuService menu = new AdvancedMenuService(
            group,
            printer,
            average,
            finder,
            validator);

        menu.Run();
    }

    static Group Seed()
    {
        return new Group
        {
            Name = "PSE-5",
            Students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Alice",
                    Email = "alice@mail.com",
                    Grades = new List<int>{8,9,10}
                },
                new Student
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@mail.com",
                    Grades = new List<int>{7,6,8}
                }
            }
        };
    }
}