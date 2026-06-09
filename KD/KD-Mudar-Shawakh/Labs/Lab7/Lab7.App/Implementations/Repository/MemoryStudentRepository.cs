using System.Collections.Generic;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations.Repository
{
    public class MemoryStudentRepository : IStudentRepository
    {
        // Private storage — same encapsulation rule from LAB-6.
        private readonly Dictionary<int, Student> _studentsById = new Dictionary<int, Student>();
        private readonly Dictionary<string, Group> _groupsByCode = new Dictionary<string, Group>();
        private readonly Faculty _faculty;

        public MemoryStudentRepository()
        {
            _faculty = new Faculty("Faculty of Technology");
            SeedData();
        }

        // ---------------------------------------------------------------
        // Sample data — mirrors the shape the API would return.
        // ---------------------------------------------------------------
        private void SeedData()
        {
            // Group CS-23 — Computer Science, 2023 cohort
            var cs23 = new Group("CS-23", "Computer Science", 2023);
            AddSeed(cs23, 1, "Alice",  "Johnson",  "alice@kauko.lt",  "Computer Science", 2023, new[] { 9, 8, 10 });
            AddSeed(cs23, 2, "Bob",    "Smith",    "bob@kauko.lt",    "Computer Science", 2023, new[] { 7, 6, 8 });
            AddSeed(cs23, 3, "Carol",  "Davis",    "carol@kauko.lt",  "Computer Science", 2023, new[] { 10, 9, 9 });

            // Group SE-24 — Software Engineering, 2024 cohort
            var se24 = new Group("SE-24", "Software Engineering", 2024);
            AddSeed(se24, 4, "David",  "Wilson",   "david@kauko.lt",  "Software Engineering", 2024, new[] { 8, 7, 9 });
            AddSeed(se24, 5, "Eva",    "Martinez", "eva@kauko.lt",    "Software Engineering", 2024, new[] { 6, 7, 5 });

            _groupsByCode[cs23.Code] = cs23;
            _groupsByCode[se24.Code] = se24;
            _faculty.AddGroup(cs23);
            _faculty.AddGroup(se24);
        }

        // Small helper — keeps SeedData() readable.
        private void AddSeed(Group group, int id, string first, string last, string email,
                             string program, int year, int[] grades)
        {
            var s = new Student(id, first, last, email, program, year);
            foreach (var g in grades) s.AddGrade(g);

            _studentsById[s.Id] = s;
            group.AddStudent(s);
        }

        // ---------------------------------------------------------------
        // IStudentRepository implementation
        // ---------------------------------------------------------------

        public IReadOnlyList<Student> GetAll()
        {
            return new List<Student>(_studentsById.Values);
        }

        public Student? GetById(int id)
        {
            return _studentsById.TryGetValue(id, out var s) ? s : null;
        }

        public void Add(Student student)
        {
            _studentsById[student.Id] = student;
        }

        public bool Remove(int id)
        {
            return _studentsById.Remove(id);
        }

        public IReadOnlyList<Group> GetAllGroups()
        {
            return new List<Group>(_groupsByCode.Values);
        }

        public Group? GetGroupByCode(string code)
        {
            return _groupsByCode.TryGetValue(code, out var g) ? g : null;
        }

        public Faculty GetFaculty()
        {
            return _faculty;
        }
    }
}