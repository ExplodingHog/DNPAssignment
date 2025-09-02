using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class CommentInMemoryRepository : ICommentRepository
    
{
    public Task<Comment> AddAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }
}