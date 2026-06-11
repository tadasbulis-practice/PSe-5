namespace Nassim.Lab3.Nassim.Service
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double AverageGrade { get; set; }

        public Student(int id, string firstName, string lastName, string email, double averageGrade)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AverageGrade = averageGrade;
        }
    }
}