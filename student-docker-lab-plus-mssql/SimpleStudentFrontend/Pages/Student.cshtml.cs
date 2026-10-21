using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleStudentFrontend.Models;

namespace SimpleStudentFrontend.Pages;

public class StudentModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public StudentModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public ApiStudentResponse? ApiStudent { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string ApiBaseUrl => _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:6001";

    public async Task OnGetAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            ApiStudent = await client.GetFromJsonAsync<ApiStudentResponse>("/api/getStudent");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
