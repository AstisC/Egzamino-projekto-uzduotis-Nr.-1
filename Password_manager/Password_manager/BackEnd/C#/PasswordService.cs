using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PasswordManagerAPI.Models;

public class PasswordService
{
    private readonly HttpClient _httpClient;

    public PasswordService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5271/api/") };
    }

    public async Task<List<PasswordEntry>> GetPasswordEntriesAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<List<PasswordEntry>>($"password/user/{userId}");
    }

    public async Task<PasswordEntry> GetPasswordEntryAsync(int entryId)
    {
        return await _httpClient.GetFromJsonAsync<PasswordEntry>($"password/{entryId}");
    }

    public async Task AddPasswordEntryAsync(PasswordEntry entry)
    {
        await _httpClient.PostAsJsonAsync("password", entry);
    }

    public async Task UpdatePasswordEntryAsync(int entryId, string newPassword)
    {
        await _httpClient.PutAsJsonAsync($"password/{entryId}", newPassword);
    }

    public async Task DeletePasswordEntryAsync(int entryId)
    {
        await _httpClient.DeleteAsync($"password/{entryId}");
    }
}
