using System.Collections.Generic;

namespace Lab7.App.Models
{
    public class Faculty
    {
        public string Name { get; }

        private readonly List<Group> _groups = new List<Group>();
        public IReadOnlyList<Group> Groups => _groups;

        public Faculty(string name)
        {
            Name = name;
        }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }
    }
}