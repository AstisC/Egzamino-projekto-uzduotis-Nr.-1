using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5271/api/") };
    }

    public async Task<bool> RegisterUserAsync(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("user/register", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> LoginUserAsync(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("user/login", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }
}
