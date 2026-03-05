using lab3.Edgar.model;
using lab3.Edgar.Service;

namespace Lab3.Edgar.Service;

public class StudentPrinter : IStudentPrinter
{
    public void PrintGroup(Group group)
    {
        foreach (var student in group.Students)
            Console.WriteLine(student.ToString());
    }
}
