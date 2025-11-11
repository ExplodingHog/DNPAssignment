using DTO;

namespace BlazorApp1.Services;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(PostCreateDto dto);
    public Task UpdatePostAsync(int id, PostUpdateDto postUpdateDto);
    public Task<PostDto?> GetPostByIdAsync(int id);
    public Task DeletePostAsync(int id);
    public  Task<List<PostDto>> GetPosts();
}