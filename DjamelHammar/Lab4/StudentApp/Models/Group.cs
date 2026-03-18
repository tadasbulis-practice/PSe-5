using System.Collections.Generic;

namespace StudentApp.Models
{
    public class Group
    {
        public List<Student> Students { get; set; } = new List<Student>();
    }
}