using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly ICommentRepository commentRepo;

    public PostsController(IPostRepository postRepo, ICommentRepository commentRepo)
    {
        this.postRepo = postRepo;
        this.commentRepo = commentRepo;
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] PostCreateDto request)
    {
        Post post = new()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId
        };
        Post createdPost = await postRepo.AddAsync(post);
        PostDto dto = new(
            createdPost.Id,
            createdPost.Title,
            createdPost.Body,
            createdPost.UserId,
            null
        );
        return Created($"/posts/{dto.Id}", dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPostById([FromRoute] int id)
    {
        Post post = await postRepo.GetSingleAsync(id);
        var comments = commentRepo.GetMany().Where(c => c.PostId == post.Id).ToList();
        PostDto dto = new(
            post.Id,
            post.Title,
            post.Body,
            post.UserId,
            comments.Select(c => new CommentDto(c.Id, c.PostId, c.UserId, c.Body)).ToList()
        );
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] int id)
    {
        await postRepo.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost([FromBody] PostUpdateDto request)
    {
        Post post = new()
        {
            Id = request.Id,
            Title = request.Title,
            Body = request.Body
        };
        await postRepo.UpdateAsync(post);
        PostDto dto = new(
            post.Id,
            post.Title,
            post.Body,
            post.UserId,
            null
        );
        return Ok(dto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetAllPosts()
    {
        var posts = postRepo.GetMany().Select(post => new PostDto(
            post.Id,
            post.Title,
            post.Body,
            post.UserId,
            null
        )).ToList();
        return Ok(posts);
    }
}