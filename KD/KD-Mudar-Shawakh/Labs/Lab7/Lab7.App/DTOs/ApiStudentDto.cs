namespace Lab7.App.DTOs
{
    // DTO = Data Transfer Object. Shape used by JSON deserialization only.
    // Lives at the boundary between the API and the domain.
    // The internal Student model does NOT inherit from or expose this.
    public class ApiStudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string StudyProgram { get; set; } = "";
        public int EnrollmentYear { get; set; }
    }
}