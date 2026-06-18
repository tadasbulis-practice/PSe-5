using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleStudentFrontend.Models;

namespace SimpleStudentFrontend.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public ApiInfoResponse? ApiInfo { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string ApiBaseUrl => _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:6001";

    public async Task OnGetAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("StudentApi");
            ApiInfo = await client.GetFromJsonAsync<ApiInfoResponse>("/api/info");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
