using lab4.model;
using lab4.Service;

namespace Lab4.Service;

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
