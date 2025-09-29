using RepositoryContracts;

namespace CLI.UI.ManageComment;

public class ListCommentView
{
    private  ICommentRepository CommentRepository;
    private ManageCommentView ManageCommentView;

    private SingleCommentView SingleCommentView;

    public ListCommentView(ICommentRepository commentRepository, ManageCommentView manageCommentView)
    {
        this.CommentRepository = commentRepository;
        this.ManageCommentView = manageCommentView;
    }

}