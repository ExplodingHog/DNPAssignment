using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace Filerepo;

public class PostFileRepo : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepo()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await GetPostsFromFile();
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        await WritePostsToFile(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await GetPostsFromFile();
        Post? existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException("post not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        await WritePostsToFile(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await GetPostsFromFile();
        Post? existingPost = posts.FirstOrDefault(p => p.Id == id);
        if (existingPost == null)
        {
            throw new InvalidOperationException("post not found");
        }

        posts.Remove(existingPost);
        await WritePostsToFile(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await GetPostsFromFile();
        Post? existingPost = posts.FirstOrDefault(p => p.Id == id);
        if (existingPost == null)
        {
            throw new InvalidOperationException("post not found");
        }

        await WritePostsToFile(posts);
        return existingPost;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts.AsQueryable();
    }

    private async Task<List<Post>> GetPostsFromFile()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
    }

    private async Task WritePostsToFile(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
}