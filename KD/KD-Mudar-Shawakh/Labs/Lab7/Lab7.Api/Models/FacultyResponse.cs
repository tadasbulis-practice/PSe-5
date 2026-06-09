namespace Lab7.Api.Models
{
    // These are the API-side response shapes. They mirror Lab7.App's DTOs
    // so JSON round-trips work without any custom mapping.
    public class FacultyResponse
    {
        public string Name { get; set; } = "";
        public List<GroupResponse> Groups { get; set; } = new();
    }

    public class GroupResponse
    {
        public string Code { get; set; } = "";
        public string StudyProgram { get; set; } = "";
        public int EnrollmentYear { get; set; }
        public List<StudentResponse> Students { get; set; } = new();
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string StudyProgram { get; set; } = "";
        public int EnrollmentYear { get; set; }
    }
}