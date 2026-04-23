namespace Lab1.App
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public void PrintInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {FirstName} {LastName}, Email: {Email}");
        }
    }
}
