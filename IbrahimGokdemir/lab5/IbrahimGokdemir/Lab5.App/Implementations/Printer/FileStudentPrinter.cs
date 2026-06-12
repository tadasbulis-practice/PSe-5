using System.IO;
using System.Text;

public class FileStudentPrinter : IStudentPrinter
{
    public void Print(Group group)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Group: {group.Name}");

        foreach (var student in group.Students)
        {
            sb.AppendLine(student.Describe());
        }

        File.WriteAllText("students-output.txt", sb.ToString());
        System.Console.WriteLine("Students written to students-output.txt");
    }
}