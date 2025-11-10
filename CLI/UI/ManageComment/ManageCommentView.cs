using RepositoryContracts;

namespace CLI.UI.ManageComment;

public class ManageCommentView
{
    private readonly ICommentRepository CommentRepository;
    private readonly CliApp CliApp;

    private ListCommentView listCommentView;
    private CreateCommentView createCommentView;

    public ManageCommentView(ICommentRepository commentRepository, CliApp cliApp)
    {
        this.CommentRepository = commentRepository;
        this.CliApp = cliApp;
    }

    public async Task ShowUI()
    {
        while (true)
        {
            Console.WriteLine("1. Create Comment");
            Console.WriteLine("2. View Post");
            Console.WriteLine("3. Go back");
            int? selectedPostId = int.Parse(Console.ReadLine());
            switch (selectedPostId)
            {
                case 1:
                    if (listCommentView == null)
                    {
                        listCommentView = new ListCommentView(CommentRepository, this);
                    }

                    
                    break;
                case 2:
                    if (createCommentView == null)
                    {
                        createCommentView = new CreateCommentView(CommentRepository, this);
                    }

                    await createCommentView.CreateComment();
                    break;
            }
        }

        await CliApp.StartAsync();
    }
}

