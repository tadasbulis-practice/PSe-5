using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleStudentFrontend.Models;

namespace SimpleStudentFrontend.Pages;

public class StudentsModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StudentsModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public List<StudentDto> Students { get; private set; } = new();
    public string? ErrorMessage { get; private set; }

    public async Task OnGetAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            Students = await client.GetFromJsonAsync<List<StudentDto>>("/api/students")
                       ?? new List<StudentDto>();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            await client.DeleteAsync($"/api/students/{id}");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return RedirectToPage();
    }
}
