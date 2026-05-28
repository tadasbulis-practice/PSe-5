using JohnBastille.Lab_4.Interfaces;

namespace JohnBastille.Lab_4.Models
{
    public class Student
    {
        private static int _nextId = 1;

        public string? Name { get; set; }
        public int Age { get; set; }
        public int Id { get; }
        public List<int> Grades { get; set; } = new();

        private readonly IAverageStrategy _averageStrategy;

        public Student(IAverageStrategy averageStrategy)
        {
            _averageStrategy = averageStrategy;
            Id = _nextId++;
        }

        public double GetAverage()
        {
            return _averageStrategy.CalculateAverage(Grades);
        }
    }
}
