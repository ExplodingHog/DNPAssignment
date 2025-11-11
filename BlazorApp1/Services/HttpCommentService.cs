using System.Text.Json;
using DTO;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    public readonly HttpClient _httpClient;

    public HttpCommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CommentDto> AddCommentAsync(CommentCreateDto dto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/comments", dto);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding Comment: {responseContent}");
        }

        return JsonSerializer.Deserialize<CommentDto>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true })!;
    }

    public async Task UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/comments/{id}", commentUpdateDto);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating Comment: {responseContent}");
        }
    }

    public async Task<CommentDto?> GetCommentByIdAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/comments/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting comment: {responseContent}");
        } 
        return JsonSerializer.Deserialize<CommentDto>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true })!;
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/comments/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting comment: {responseContent}");
        } 
    }

    public async Task<IQueryable<CommentDto>> GetComments()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/comments");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting comment: {responseContent}");
        } 
        var comments = JsonSerializer.Deserialize<List<CommentDto>>(responseContent, new JsonSerializerOptions 
            { PropertyNameCaseInsensitive = true })!;
        return comments.AsQueryable();
    }
}