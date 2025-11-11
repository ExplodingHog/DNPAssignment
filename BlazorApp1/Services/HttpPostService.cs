using System.Text.Json;
using DTO;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    
    public readonly HttpClient _httpClient;

    public HttpPostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<PostDto> AddPostAsync(PostCreateDto dto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("posts", dto);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error adding post: {responseContent}");
        }
        return JsonSerializer.Deserialize<PostDto>(responseContent, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true })!;
    }

    public async Task UpdatePostAsync(int id, PostUpdateDto postUpdateDto)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"posts/{id}", postUpdateDto);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error updating post: {responseContent}");
        } 
        
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"posts/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting post: {responseContent}");
        } 
        return JsonSerializer.Deserialize<PostDto>(responseContent, new JsonSerializerOptions 
            { PropertyNameCaseInsensitive = true });
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"Posts/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error deleting post: {responseContent}");
        } 
    }

    public async Task<List<PostDto>> GetPosts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("posts");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error getting all post: {responseContent}");
        } 
        var posts = JsonSerializer.Deserialize<List<PostDto>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }
}