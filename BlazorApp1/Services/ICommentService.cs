using DTO;

namespace BlazorApp1.Services;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CommentCreateDto dto);
    public Task UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto);
    public Task<CommentDto?> GetCommentByIdAsync(int id);
    public Task DeleteCommentAsync(int id);
    public  Task<IQueryable<CommentDto>> GetComments();
}