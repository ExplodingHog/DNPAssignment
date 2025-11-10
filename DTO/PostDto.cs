namespace DTO;


    public record PostCreateDto(String Title, String Body, int UserId);
    public record PostUpdateDto(int Id, String Title, String Body, int UserId);
    public record PostDto(int Id, String Title, String Body, int UserId, IEnumerable<CommentDto>? Comments);
