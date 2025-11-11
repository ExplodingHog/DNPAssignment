using System.Text.Json;
using DTO;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    public readonly HttpClient _httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<UserDto> AddUserAsync(LoginDto dto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users", dto);
        String responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding user: {responseContent}");
        }

        return JsonSerializer.Deserialize<UserDto>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"users/{id}", updateUserDto);
        String responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating user: {responseContent}");
        }
        
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"users/{id}");
        String responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting user: {responseContent}");
        }
        return JsonSerializer.Deserialize<UserDto>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{id}");
        String responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting user: {responseContent}");
        }
    }

    public async  Task<List<UserDto>> GetUsers()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("users");
        String responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting users: {responseContent}");
        }

        return JsonSerializer.Deserialize<List<UserDto>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
    }
}