using System.Reflection.Metadata;
using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = new();
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(x => x.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
      User? existingUser = users.FirstOrDefault(x => x.Id == user.Id);
      if (existingUser == null)
      {
          throw new Exception("User not found");
      }
      users.Remove(existingUser);
      users.Add(user);
      return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new Exception($"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new Exception($"User with ID '{id}' not found");
        }
        
        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public Task VerifyUserNameIsAvailableAsync(string userName)
    {
        {
            bool exists = users.Any(u => u.username.Equals(
                userName, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                throw new InvalidOperationException(
                    $"UserName '{userName}' is already taken");
            }

            return Task.CompletedTask;
        }
    }
}