using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace Filerepo;

public class CommentFileRepo : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepo()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
       List<Comment> comments = await GetCommentsFromFile();
       Comment? existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
       if (existingComment != null)
       {
           throw new InvalidOperationException("comment not found");
       }
       
       comments.Remove(existingComment);
       comments.Add(comment);
       await WriteCommentsToFile(comments);
    }

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments =  await GetCommentsFromFile();
        Comment? existingComment = comments.FirstOrDefault(c => c.Id == id);
        if (existingComment != null)
        {
            throw new InvalidOperationException("comment not found");
        }
        else
        {
            comments.Remove(existingComment);
            await WriteCommentsToFile(comments);
        }
    }

    public  async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await GetCommentsFromFile();
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);
        if (existingComment == null)
        {
            throw new InvalidOperationException("comment not found");
        }

        await WriteCommentsToFile(comments);
        return existingComment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllText(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
    
    
    private async Task<List<Comment>> GetCommentsFromFile()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }

    private async Task WriteCommentsToFile(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}