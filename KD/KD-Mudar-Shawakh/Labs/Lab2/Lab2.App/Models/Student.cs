namespace Lab2.App.Models
{
	public class Student
	{
		public string Name { get; }
		private List<int> Grades { get; }

		public Student(string name)
		{
			Name = name;
			Grades = new List<int>();
		}

		public void AddGrade(int grade)
		{
			Grades.Add(grade);
		}

		public double CalculateAverage()
		{
			if (Grades.Count == 0)
				return 0;

			return Grades.Average();
		}

		public string Describe()
		{
			return $"Student: {Name}, Average = {CalculateAverage():F2}";
		}
	}
}
