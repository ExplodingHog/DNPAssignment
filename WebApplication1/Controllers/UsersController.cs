using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        await _userRepository.VerifyUserNameIsAvailableAsync(request.UserName);
        User user = new(request.UserName, request.Password);
        User createdUser = await _userRepository.AddAsync(user);
        UserDto dto = new()
        {
            Id = createdUser.Id,
            UserName = createdUser.username
        };
        return Created($"/users/{dto.Id}", createdUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUserById([FromBody] UpdateUserDto updateUserDto)
    {       
        User user = new(updateUserDto.UserName, updateUserDto.Password)
        {
            Id = updateUserDto.Id
        };
        await _userRepository.UpdateAsync(user);
        UserDto dto = new()
        {
            Id = user.Id,
            UserName = user.username
        };
        return Ok(dto);
     
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        await _userRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleUser([FromRoute] int id)
    {
        User user = await _userRepository.GetSingleAsync(id);
        UserDto dto = new()
        {
            Id = user.Id,
            UserName = user.username
        };
        return Ok(dto);
    }

    [HttpGet]
    public ActionResult<List<UserDto>> GetAllUsers()
    {
        IQueryable<User> users = _userRepository.GetMany();
        List<UserDto> dtos = users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.username
        }).ToList();
        return Ok(dtos);
    }
}