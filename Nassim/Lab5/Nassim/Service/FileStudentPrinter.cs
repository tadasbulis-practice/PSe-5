using System.IO;

namespace Nassim.Lab4.Nassim.Service
{
    public class FileStudentPrinter : IStudentPrinter
    {
        private readonly string _filePath;

        public FileStudentPrinter(string filePath)
        {
            _filePath = filePath;
        }

        public void Print(Group group)
        {
            using var writer = new StreamWriter(_filePath, append: false);
            writer.WriteLine($"=== Group: {group.Name} ===");
            foreach (var s in group.Students)
                writer.WriteLine($"[{s.Id}] {s.FirstName} {s.LastName} | {s.Email} | Avg: {s.AverageGrade}");
        }
    }
}