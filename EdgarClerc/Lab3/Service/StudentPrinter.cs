using lab3.model;
using lab3.Service;

namespace Lab3.Service;

public class StudentPrinter : IStudentPrinter
{
    public string PrintGroup(Group group)
    {
        string str = "";
        foreach (var student in group.Students)
            str += student + "\n";

        return str;
    }
}
