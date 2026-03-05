using lab3.model;
using lab3.Service;

namespace Lab3.Service;

public class StudentPrinter : IStudentPrinter
{
    public void PrintGroup(Group group)
    {
        foreach (var student in group.Students)
            Console.WriteLine(student.ToString());
    }
}
