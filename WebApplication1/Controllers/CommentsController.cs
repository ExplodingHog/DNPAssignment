using DTO;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentCreateDto request)
    {
        Comment comment = new()
        {
            Body = request.Body,
            PostId = request.PostId,
            UserId = request.UserId
        };
        Comment createdComment = await _commentRepository.AddAsync(comment);
        CommentDto dto = new (
            createdComment.Id,
             createdComment.PostId,
            createdComment.UserId,
             createdComment.Body
        );

        return Created($"/comments/{dto.Id}", dto);
    }
    
    [HttpPut("{id}")] 
    public async Task<ActionResult<CommentDto>> UpdateComment( [FromBody] CommentUpdateDto request)
    {
        Comment comment = new()
        {
            Id = request.Id,
            Body = request.Body
        };
        await _commentRepository.UpdateAsync(comment);
        CommentDto dto = new (
            comment.Id,
            comment.PostId,
            comment.UserId,
            comment.Body
        );
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        await _commentRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleComment([FromRoute] int id)
    {
        Comment comment = await _commentRepository.GetSingleAsync(id);
        CommentDto dto = new(
            comment.Id,
            comment.PostId,
            comment.UserId,
            comment.Body
        );
        return Ok(dto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetAllComments()
    {
        var comments = _commentRepository.GetMany().Select(comment => new CommentDto(
            comment.Id,
            comment.PostId,
            comment.UserId,
            comment.Body
        ));
        return Ok(comments);
    }
    
}