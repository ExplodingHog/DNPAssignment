using Entities;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
  
    private readonly Task<Post> post;
    private readonly ListPostsView listPostsView;

   

    public SinglePostView(Task<Post> post,
        ListPostsView listPostsView)
    {
       
        this.post = post;
        this.listPostsView = listPostsView;
    }

    public async Task ShowPost()
    {
        Console.WriteLine(
            $"'{post.Result.Title}'[{post.Result.Id}] by User [{post.Result.UserId}]" +
            $"\n{post.Result.Body}\n");
        Console.WriteLine("1. Edit post");
        Console.WriteLine("2. delete post");
        Console.WriteLine("3.  Go back");
        int? selection = int.Parse(Console.ReadLine());
        switch (selection)
        {
            case 1:  break;
            case 2:  break;
            default: await listPostsView.ListPosts(); break;
        }
    }
}