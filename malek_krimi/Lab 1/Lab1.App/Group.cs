using System.Collections.Generic;

namespace Lab1.App
{
    public class Group
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Group(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void PrintAll()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("No students in the group.");
                return;
            }

            Console.WriteLine($"Group: {Name}");
            foreach (var student in Students)
            {
                student.PrintInfo();
            }
        }
    }
}
