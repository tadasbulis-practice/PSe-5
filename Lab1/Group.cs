namespace ConsoleApp1;

public class Group
{
    public string name { get; set; }
    private List<StudentProfile> students = new List<StudentProfile>();

    public void addStudent(StudentProfile student)
    {
        students.Add(student);
    }

    public void printAll()
    {
        foreach (var student in students)
        {
            Console.WriteLine(student.toString());
        }
    }
    
}