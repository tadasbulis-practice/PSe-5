using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleStudentFrontend.Models;

namespace SimpleStudentFrontend.Pages;

public class StudentCreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StudentCreateModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    [BindProperty]
    public StudentDto Input { get; set; } = new();

    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            var response = await client.PostAsJsonAsync("/api/students", Input);
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
