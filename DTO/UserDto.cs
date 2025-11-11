namespace DTO;

public record UserDto
{

    public required int Id { get; set; }
    public required string UserName { get; set; }
}

public record LoginDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
