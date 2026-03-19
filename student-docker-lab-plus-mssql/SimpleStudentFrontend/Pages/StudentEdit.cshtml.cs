using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleStudentFrontend.Models;

namespace SimpleStudentFrontend.Pages;

public class StudentEditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StudentEditModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    [BindProperty]
    public StudentDto Input { get; set; } = new();

    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            var student = await client.GetFromJsonAsync<StudentDto>($"/api/students/{id}");
            if (student is null) return NotFound();
            Input = student;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            var response = await client.PutAsJsonAsync($"/api/students/{Input.Id}", Input);
            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Students");

            ErrorMessage = $"API error: {response.StatusCode}";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return Page();
    }
}
