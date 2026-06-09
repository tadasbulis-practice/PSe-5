using System.Collections.Generic;

namespace Lab7.App.DTOs
{
    public class ApiFacultyDto
    {
        public string Name { get; set; } = "";
        public List<ApiGroupDto> Groups { get; set; } = new List<ApiGroupDto>();
    }
}