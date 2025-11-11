using DTO;

namespace BlazorApp1.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(LoginDto dto);
    public Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    public Task<UserDto?> GetUserByIdAsync(int id);
    public Task DeleteUserAsync(int id);
    public  Task<List<UserDto>> GetUsers();
    
}