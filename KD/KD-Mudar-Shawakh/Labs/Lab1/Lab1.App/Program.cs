//Random random = new Random();

//int task = random.Next(1, 6);

//Console.WriteLine("task is: " + task);

//Console.WriteLine("press any");
//Console.ReadKey();

public class Student { 
    public string Name { get; set; }
    public List<int> Grades { get; set; }

    public Student(string name)
    {
        Name = name;
        Grades = new List<int>();
    }

    public double getAvarage()
    {
        if (Grades.Count == 0)
        {
            return 0;
        }
        int sum = 0;
        foreach(int grade in Grades)
        {
            sum += grade;
        }
        return (double)sum / Grades.Count;
    }

    public void printinfo()
    {
        double avg = getAvarage();
        Console.WriteLine("Student: " + Name + " Avarage = " + getAvarage());
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student s1 = new Student("student 1");
        s1.Grades.Add(75);
        s1.Grades.Add(80);
        s1.Grades.Add(90);
        s1.Grades.Add(65);

        Student s2 = new Student("student 2");
        s2.Grades.Add(100);
        s2.Grades.Add(92);
        s2.Grades.Add(73);
        s2.Grades.Add(86);

        Student s3 = new Student("student 3");
        s3.Grades.Add(83);
        s3.Grades.Add(81);
        s3.Grades.Add(97);
        s3.Grades.Add(79);

        s1.printinfo();
        s2.printinfo();
        s3.printinfo();

        Console.WriteLine("Press any ");
        Console.ReadKey();
    }
}