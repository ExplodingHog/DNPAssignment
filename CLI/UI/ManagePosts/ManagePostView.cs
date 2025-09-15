using System.Runtime.InteropServices.Marshalling;
using CLI.UI.ManagePosts;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ManagePostView
{
    private readonly IPostRepository postRepository;
    
    private readonly CliApp cliApp;
    
    
    private ListPostsView? listPostsView;
    private CreatePostView createPostView;

    public ManagePostView(IPostRepository postRepository,  CliApp cliApp)
    {
        this.postRepository = postRepository;
       
        this.cliApp = cliApp;
    }

    public async Task ShowUi()
    {
        while (true)
        {
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. View Post");
            Console.WriteLine("3. Go back");
            int? selectedPostId = int.Parse(Console.ReadLine());
            switch (selectedPostId)
            {
                case 1:
                    if (listPostsView == null)
                    {
                        listPostsView = new ListPostsView(postRepository,  this); 
                    }

                    await listPostsView.ListPosts();
                    break;
                case 2:
                    if (createPostView == null)
                    {
                        createPostView = new CreatePostView(postRepository, this);
                    }

                    await createPostView.CreatePost();
                    break;
            }
        }

        await cliApp.StartAsync();
    }
}