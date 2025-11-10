using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace Filerepo;

public class UserFileRepo : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepo()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        List<User> users = await GetUsersFromFile();
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
        user.Id = maxId + 1;
        users.Add(user);
        await WriteUsersToFile(users);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await GetUsersFromFile();
        User? existingUser = users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("user not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        await WriteUsersToFile(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await GetUsersFromFile();
        User? existingUser = users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("user not found");
        }

        users.Remove(existingUser);
        await WriteUsersToFile(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await GetUsersFromFile();
        User? existingUser = users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("user not found");
        }

        return existingUser;
    }

    public IQueryable<User> GetMany()
    {
        String userJson= File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(userJson)!;
        return users.AsQueryable();
    }

    private async Task WriteUsersToFile(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task VerifyUserNameIsAvailableAsync(string userName)
    {
        List<User> users = await GetUsersFromFile();
        bool userNameExists = users.Any(u => u.username == userName);
        if (userNameExists)
        {
            throw new InvalidOperationException(
                $"UserName '{userName}' is already taken.");
        }
    }

    private async Task<List<User>> GetUsersFromFile()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
    }
}