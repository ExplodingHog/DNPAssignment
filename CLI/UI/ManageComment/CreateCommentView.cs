using RepositoryContracts;

namespace CLI.UI.ManageComment;

public class CreateCommentView
{
    private readonly ICommentRepository CommentRepository;
    private readonly ManageCommentView ManageCommentView;

    public CreateCommentView(ICommentRepository commentRepository, ManageCommentView manageCommentView)
    {
        this.CommentRepository = commentRepository;
        this.ManageCommentView = manageCommentView;
    }
    public async Task CreateComment()
    {
        Console.WriteLine("Creating a new post...");
        Console.WriteLine("Enter your user ID:"); //temp until login implemented
        int userId = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter post ID:");
        int postId = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the body:");
        string body = Console.ReadLine();

        await CommentRepository.AddAsync(new Entities.Comment
        {
            PostId = postId,
            Body = body,
            UserId = userId
        });
        Console.WriteLine(
            $"Post created successfully with ID: {CommentRepository.GetMany().Last().Id}");
        await ManageCommentView.ShowUI();
    }
    
}