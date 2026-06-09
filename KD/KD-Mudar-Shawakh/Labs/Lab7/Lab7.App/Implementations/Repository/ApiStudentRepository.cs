using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lab7.App.DTOs;
using Lab7.App.Interfaces;
using Lab7.App.Models;

namespace Lab7.App.Implementations.Repository
{
    /// <summary>
    /// Loads faculty/group/student data from a REST API on construction,
    /// then serves it through the same IStudentRepository contract
    /// MemoryStudentRepository uses.
    ///
    /// SOLID demonstration: swapping Memory -> Api requires changing ONE
    /// line in Program.cs. StudentService is untouched.
    /// </summary>
    public class ApiStudentRepository : IStudentRepository
    {
        // INTERNAL complexity — HttpClient, Dictionaries, DTOs — all private.
        // OUTSIDE the class only sees the clean IStudentRepository surface.
        private readonly Dictionary<int, Student> _studentsById = new Dictionary<int, Student>();
        private readonly Dictionary<string, Group> _groupsByCode = new Dictionary<string, Group>();
        private Faculty _faculty = new Faculty("Unknown Faculty");

        private readonly HttpClient _http;

        public ApiStudentRepository(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };

            // Constructor blocks on the load — for a console app this is fine.
            // A web app would call LoadFromApi() async during startup instead.
            LoadFromApi().GetAwaiter().GetResult();
        }

        private async Task LoadFromApi()
        {
            Console.WriteLine("  [Api] Connecting to backend...");

            try
            {
                var dto = await _http.GetFromJsonAsync<ApiFacultyDto>("/api/faculty");

                if (dto == null)
                {
                    Console.WriteLine("  [Api] WARNING: API returned null.");
                    return;
                }

                MapDtoToDomain(dto);

                Console.WriteLine($"  [Api] Loaded {_studentsById.Count} students " +
                                  $"across {_groupsByCode.Count} groups.\n");
            }
            catch (Exception ex)
            {
                // Don't let a network failure crash the demo —
                // we fall through with empty data and show a clear error.
                Console.WriteLine($"  [Api] ERROR: {ex.Message}");
                Console.WriteLine("  [Api] Make sure Docker is running: docker compose up\n");
            }
        }

        // Maps API DTO shape to internal domain shape.
        // This is the ONLY place DTOs ever touch domain objects.
        private void MapDtoToDomain(ApiFacultyDto dto)
        {
            _faculty = new Faculty(dto.Name);

            foreach (var groupDto in dto.Groups)
            {
                var group = new Group(groupDto.Code, groupDto.StudyProgram, groupDto.EnrollmentYear);

                foreach (var sDto in groupDto.Students)
                {
                    var student = new Student(
                        sDto.Id, sDto.FirstName, sDto.LastName,
                        sDto.Email, sDto.StudyProgram, sDto.EnrollmentYear);

                    _studentsById[student.Id] = student;
                    group.AddStudent(student);
                }

                _groupsByCode[group.Code] = group;
                _faculty.AddGroup(group);
            }
        }

        // ---------------------------------------------------------------
        // IStudentRepository — exact same implementation shape as Memory.
        // That symmetry IS the LSP point: the two are substitutable.
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