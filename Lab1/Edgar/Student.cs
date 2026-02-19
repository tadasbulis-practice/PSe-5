namespace ConsoleApp1;

public class StudentProfile
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Group { get; set; }
  public DateOnly LectureDate { get; set; }
  
  public List<double> Grades { get; set; }

  
  private double average() => Grades.Average();

  public void printInfo()
  {
    Console.WriteLine($"{FirstName} {LastName} Average Grades :  {average()}");
  }
  public string toString()
  {
    return "name: " + FirstName + " " + LastName + ", group: " + Group + ", lecture date: " + LectureDate;
  }
}

