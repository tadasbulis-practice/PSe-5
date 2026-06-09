using System.Collections.Generic;

namespace Lab5.App.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public Group(string name)
        {
            Name = name;
        }
    }
}