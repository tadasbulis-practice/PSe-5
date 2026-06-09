using System.Collections.Generic;

namespace Lab7.App.DTOs
{
    public class ApiGroupDto
    {
        public string Code { get; set; } = "";
        public string StudyProgram { get; set; } = "";
        public int EnrollmentYear { get; set; }
        public List<ApiStudentDto> Students { get; set; } = new List<ApiStudentDto>();
    }
}