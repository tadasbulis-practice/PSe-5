using System.Collections.Generic;

namespace Nassim.Lab4.Nassim.Service
{
    public class GroupService
    {
        private readonly IStudentPrinter _printer;

        public GroupService(IStudentPrinter printer)
        {
            _printer = printer;
        }

        public void DisplayGroup(Group group)
        {
            _printer.PrintStudents(group.Students);
        }
    }
}