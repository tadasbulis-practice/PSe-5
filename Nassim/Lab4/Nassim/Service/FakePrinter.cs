using System;

namespace Nassim.Lab4.Nassim.Service{
    public class FakePrinter : IStudentPrinter
    {
        public void Print(Group group)
        {
            Console.WriteLine("[FAKE] Group: " + group.Name);
            Console.WriteLine("[FAKE] Students: " + group.Students.Count);
        }
    }
}